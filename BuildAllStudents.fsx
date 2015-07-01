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
    
let branchBuildDir branch = buildDir + branch + "/"

let createBuildTarget branch =
    TargetTemplate (fun _ ->
       !! "**/*.sln"
         |> MSBuildDebug (branchBuildDir branch) "Build"
         |> Log "Build-Output: "
    ) ("Build_" + branch) ()

let createTestTarget branch =
    TargetTemplateWithDependencies ["Build_" + branch ] (fun _ ->
        !! (branchBuildDir branch + "*.Tests.dll")
          |> NUnit (fun p ->
              {p with
                 DisableShadowCopy = true;
                 ToolPath = "c:/Program Files (x86)/NUnit 2.6.4/bin";
                 OutputFile = branchBuildDir branch + "TestResults.xml" })
    ) ("Test_" + branch) ()

Target "Default" (fun _ ->
    let branches = getRemoteBranches gitRepositoryDir
    branches
    |> List.filter (fun x -> not (x.StartsWith("origin")))
    |> List.map (fun branch -> 
        checkoutBranch gitRepositoryDir branch
        let remote = branch.Split('/').[0]
        trace (sprintf "STARTING build for : %s" remote)
        createBuildTarget remote
        createTestTarget remote
        run ("Test_" + remote)
    )
    |> ignore
)

"Clean"
    ==> "Default"

RunTargetOrDefault "Default"