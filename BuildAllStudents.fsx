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
//        try
           !! "**/*.sln"
             |> MSBuildDebug (branchBuildDir branch) "Build"
             |> Log "Build-Output: "
//        with
//            | _ -> ignore ()
        
    ) ("Build_" + branch) ()

let createTestTarget branch =
    TargetTemplateWithDependencies ["Build_" + branch ] (fun _ ->
        !! (branchBuildDir branch + "*.Tests.dll")
          |> NUnit (fun p ->
              {p with
                 ErrorLevel = TestRunnerErrorLevel.DontFailBuild
                 DisableShadowCopy = true;
                 ToolPath = "c:/Program Files (x86)/NUnit 2.6.4/bin";
                 OutputFile = branchBuildDir branch + "TestResults.xml" })
    ) ("Test_" + branch) ()

Target "BuildAndTestAllBranches" (fun _ ->
    let branches = getRemoteBranches gitRepositoryDir
    printfn "Nombres de repos GitHub : %i" branches.Length
    branches
    |> List.filter (fun x -> not (List.exists (fun (y:string) -> y = x) ["Aurélien/master"; "Laurianne/master"]))
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

type Note = {
    Name: string;
    NbTestsSucceeded: float;
    NbTestsFailures: float;
    NbTestsErrors: float
}

Target "DisplayResults" (fun _ ->
    !! (buildDir + "**/TestResults.xml")
    |> Seq.map (fun x -> 
        let directory = (FileHelper.directory x).Split('\\')
        let who = directory.[directory.Length - 1]
        let xmlDoc = new System.Xml.XmlDocument()
        xmlDoc.Load x
        let total = XPathValue "//test-results/@total" [] xmlDoc
        let failures = XPathValue "//test-results/@failures" [] xmlDoc
        let errors = XPathValue "//test-results/@errors" [] xmlDoc
        { Name = who; NbTestsSucceeded = float total - float failures - float errors; NbTestsErrors = float errors; NbTestsFailures = float failures }
    )
    |> Seq.sortBy (fun note -> note.Name)
    |> Seq.iter (fun note -> printfn "%s\t%s%f\t%f\t%f" note.Name (if note.Name.Length >= 8 then "" else "\t") ((note.NbTestsSucceeded + 0.5 * note.NbTestsFailures + 0.2 * note.NbTestsErrors)*17.0/20.0) note.NbTestsFailures note.NbTestsErrors)
)

"Clean"
    ==> "BuildAndTestAllBranches"
    ==> "DisplayResults"

RunTargetOrDefault "DisplayResults"