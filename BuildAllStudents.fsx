#if INTERACTIVE
    System.Environment.CurrentDirectory <- @"C:\Dev\IUTLyon1FinalTest";;
#endif

#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git

RestorePackages

let gitRepositoryDir = currentDirectory;
let buildDir = "./build/"

Target "Clean" (fun _ -> 
    CleanDir buildDir
)

Target "Default" (fun _ ->
    let branches = getRemoteBranches gitRepositoryDir
    branches
    |> List.filter (fun x -> not (x.StartsWith("origin")))
    |> List.map (fun branch -> 
        ExecutedTargets.Clear()
        checkoutBranch gitRepositoryDir branch
        trace (sprintf "STARTING build on branch : %s" branch)
        run "DefaultBranch"
    )
    |> ignore
)

"Clean"
    ==> "Default"
    
let branchBuildDir = fun () -> buildDir + (getBranchName gitRepositoryDir).Split('/').[0] + "/"

Target "Build" (fun _ ->
   !! "**/*.sln"
     |> MSBuildDebug (branchBuildDir()) "Build"
     |> Log "Build-Output: "
)

Target "Test" (fun _ ->
    !! (branchBuildDir() + "*.Tests.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             ToolPath = "c:/Program Files (x86)/NUnit 2.6.4/bin";
             OutputFile = branchBuildDir() + "TestResults.xml" })
)

Target "DefaultBranch" (fun _ -> 
    let branch = getBranchName gitRepositoryDir
    trace (sprintf "DONE build on branch : %s" branch)
)

"Build"
    ==> "Test"
    ==> "DefaultBranch"

RunTargetOrDefault "Default"