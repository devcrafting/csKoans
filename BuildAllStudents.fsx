﻿#if INTERACTIVE
    System.Environment.CurrentDirectory <- @"C:\Dev\IUTLyon1FinalTest";;
#endif

#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git
open Fake.DotCover
open System

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

let buildParamsAndExecute parameters buildArguments toolPath workingDir =
    let args = buildArguments parameters
    trace (toolPath + " " + args)
    ExecProcess (fun info ->  
              info.FileName <- toolPath
              info.WorkingDirectory <- getWorkingDir workingDir
              info.Arguments <- args) TimeSpan.MaxValue |> ignore
    
let DotCoverNoFail (setParams: DotCoverParams -> DotCoverParams) =
    let parameters = (DotCoverDefaults |> setParams)
    buildParamsAndExecute parameters buildDotCoverArgs parameters.ToolPath parameters.WorkingDir

let DotCoverNUnitNoFail (setDotCoverParams: DotCoverParams -> DotCoverParams) (setNUnitParams: NUnitParams -> NUnitParams) (assemblies: string seq) = 
    let assemblies = assemblies |> Seq.toArray
    let details =  assemblies |> separated ", "
    traceStartTask "DotCoverNUnit" details

    let parameters = NUnitDefaults |> setNUnitParams
    let args = buildNUnitdArgs parameters assemblies
    
    DotCoverNoFail (fun p ->
                  {p with
                     TargetExecutable = parameters.ToolPath @@ parameters.ToolName
                     TargetArguments = args
                  } |> setDotCoverParams)

    traceEndTask "DotCoverNUnit" details

let createTestTarget branch =
    TargetTemplateWithDependencies ["Build_" + branch ] (fun _ ->
        !! (branchBuildDir branch + "*.Tests.dll")
          |> DotCoverNUnitNoFail 
            (fun p -> 
                { p with 
                    ToolPath = @"C:\Users\Administrator\AppData\Local\JetBrains\Installations\dotCover02\dotCover.exe"
                    Output = branchBuildDir branch + "dotCover.dcvr"
                    Filters = "-:module=*;type=*Tests" })
            (fun p ->
              {p with
                 ErrorLevel = TestRunnerErrorLevel.DontFailBuild
                 DisableShadowCopy = true;
                 ToolPath = "c:/Program Files (x86)/NUnit 2.6.4/bin";
                 OutputFile = branchBuildDir branch + "TestResults.xml" })
    ) ("Test_" + branch) ()
    
let createSonarTarget branch =
    TargetTemplateWithDependencies ["Test_" + branch ] (fun _ ->
        DotCoverReport (fun p -> 
            { p with 
                Source = branchBuildDir branch + "dotCover.dcvr"                
                ToolPath = @"C:\Users\Administrator\AppData\Local\JetBrains\Installations\dotCover02\dotCover.exe"
                ReportType = DotCoverReportType.Html; Output = branchBuildDir branch + "dotCover.html" })
        let sonarRunner = { 
                defaultParams with
                WorkingDirectory = currentDirectory
                Program = @"C:\Dev\sonar-runner-2.4\bin\sonar-runner.bat"
                Args = [ ("-Dsonar.projectKey=IUTLyon1:2015:" + branch.Replace("é","e"), "");
                    ("-Dsonar.projectName=\"IUTLyon1 2015 - " + branch + "\"", "");
                    ("-Dsonar.projectVersion=v1", "");
                    ("-Dsonar.sources=.", "");
                    ("-Dsonar.exclusions=**/*Tests.cs", "");
                    ("-Dsonar.cs.dotcover.reportsPaths=**/" + branch + "/dotCover.html", "");
                    ("-Dsonar.cs.nunit.reportsPaths=**/" + branch + "/TestResults.xml", "")]
            }
        shellExec sonarRunner |> ignore
    ) ("Sonar_" + branch) ()

Target "BuildAndTestAllBranches" (fun _ ->
    let branches = getRemoteBranches gitRepositoryDir
    printfn "Nombres de repos GitHub : %i" branches.Length
    branches
    |> List.filter (fun x -> not (List.exists (fun (y:string) -> y = x) ["Aurélien/master"; "Laurianne/master"]))
    |> List.append ["Aurélien"; "Laurianne"]
    |> List.filter (fun x -> not (x.StartsWith("origin")))
    |> List.map (fun branch -> 
        checkoutBranch gitRepositoryDir branch
        let remote = branch.Split('/').[0]
        trace (sprintf "STARTING build for : %s" remote)
        createBuildTarget remote
        createTestTarget remote
        createSonarTarget remote
        run ("Sonar_" + remote)
    )
    |> ignore
)

type Note = {
    Name: string;
    NbTestsSucceeded: float;
    NbTestsFailures: float;
    NbTestsErrors: float;
    NbSucceededLinq: float
}

open System.Xml.XPath

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
        let xpathNav = xmlDoc.CreateNavigator()
        let succeededLinq = float (xpathNav.Evaluate("count(//test-suite[@name='LinqTests']//test-case[@result='Success']/@result)").ToString())
        { Name = who; NbTestsSucceeded = float total - float failures - float errors; NbTestsErrors = float errors; NbTestsFailures = float failures; NbSucceededLinq = succeededLinq }
    )
    |> Seq.sortBy (fun note -> note.Name)
    |> Seq.iter (fun note -> 
                    printfn "%s\t%s%f\t%f\t%f\t%f" 
                        note.Name 
                        (if note.Name.Length >= 8 then "" else "\t") 
                        ((note.NbTestsSucceeded - note.NbSucceededLinq/2.0 + 0.5 * note.NbTestsFailures + 0.2 * note.NbTestsErrors)*17.0/16.0)
                        note.NbTestsFailures 
                        note.NbTestsErrors 
                        note.NbSucceededLinq)
)

"Clean"
    ==> "BuildAndTestAllBranches"
    ==> "DisplayResults"

Target "SonarSolution" (fun _ ->
    checkoutBranch gitRepositoryDir "solution"
    createBuildTarget "solution"
    createTestTarget "solution"
    createSonarTarget "solution"
    run "Sonar_solution"
)

RunTargetOrDefault "DisplayResults"