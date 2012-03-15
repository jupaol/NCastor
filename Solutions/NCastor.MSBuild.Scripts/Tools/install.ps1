param($installPath, $toolsPath, $package, $project)

$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
$AutoMagicBuilder = $solution.AddSolutionFolder("AutoMagicBuilder")
$AutoMagicBuilderObject = Get-Interface $AutoMagicBuilder.Object ([EnvDTE80.SolutionFolder])
$AutoMagicBuilder_Tasks = $AutoMagicBuilderObject.AddSolutionFolder("Tasks")