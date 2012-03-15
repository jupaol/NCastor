param($installPath, $toolsPath, $package, $project)

foreach ($item in $solution.projects) 
{ 
	if($item.type -eq "Unknown" -and $item.name -eq "AutoMagicBuilder") 
	{ 
		$item.delete() 
	} 
}
