param($installPath, $toolsPath, $package, $project)


# bd nuget help page
start 'http://captcha.com/doc/aspnet/captcha-getting-started-nuget.html?utm_source=nuget&utm_medium=nuget&utm_campaign=4.4.0.0'


# get asp.net mvc version used by the project, if any
try {
  $mvcRef = $project.Object.References | where{$_.Name -eq "System.Web.Mvc"}
  if ($mvcRef -eq $null) {
    #Write-Host "asp.net mvc reference not found"
  } else {
    $mvcVersion = $mvcRef.Version
    if ($mvcVersion -eq $null) {
      #Write-Host "asp.net mvc reference found but can't detect version"
    } else {
      #Write-Host $mvcVersion
    }
  }
} catch {}
if ($mvcVersion -eq $null) {
  Exit
}


# find web.config file
try {
  if ($project.ProjectItems) {
    #Write-Host "project items found"
    $file = $project.ProjectItems.Item("web.config")
    if ($file.Properties) {
      #Write-Host "web.config project item found"
      $localPath = $file.Properties.Item("LocalPath")
      if ($localPath) {
        $localPath = $localPath.Value
        #Write-Host $localPath
      } else {
        #Write-Host "can't read web.config local path"
      }
    } else {
      #Write-Host "no web.config project item"
    }
  } else {
    #Write-Host "no project items"
  }
} catch {}
if ($localPath -eq $null) {
  Exit
}


# let nuget add dependencies it detects to web.config first
Add-BindingRedirect $project.Name


# parse web.config content
$xml = New-Object xml
$xml.Load($localPath)

# find or insert runtime node
$runtimeNode = Select-XML -Xml $xml -XPath "//runtime"
if ($runtimeNode) {
  #Write-Host "runtime element found"
  $runtimeNode = $runtimeNode.Node
} else {
  #Write-Host "no runtime element"
  $runtimeNode = $xml.CreateElement("runtime")
  $xml.Configuration.AppendChild($runtimeNode)
}

# find or insert assemblyBinding node
$assemblyNode = Select-XML -Xml $xml -XPath "//runtime//*[local-name()='assemblyBinding']"
if ($assemblyNode) {
  #Write-Host "assemblyBinding element found"
  $assemblyNode = $assemblyNode.Node
} else {
  #Write-Host "no assemblyBinding element"
  $assemblyNode = $xml.CreateElement("assemblyBinding")
  $assemblyNode.SetAttribute("xmlns", "urn:schemas-microsoft-com:asm.v1")
  $runtimeNode.AppendChild($assemblyNode)
}

# try to find the asp.net mvc binding redirect and update the version number
$bindingNodes = Select-XML -Xml $xml -XPath "//runtime//*[local-name()='assemblyBinding']/*[local-name()='dependentAssembly']"
$found = 0
$updated = 0
ForEach ($i in $bindingNodes) {
  ForEach ($c in $i.Node.ChildNodes) {
    if ($found -eq 0) { # mvc dependent assembly node not found yet
      if ($c -and ($c.GetAttribute("name") -eq "System.Web.Mvc")) { # detect assembly identity node
        $found = 1
      }
    } else { # mvc dependent assembly node already found
      if ($updated -eq 0) { # binding redirect node immediately after the detected assembly identity node
        $c.SetAttribute("oldVersion", "0.0.0.0-" + $mvcVersion)
        $c.SetAttribute("newVersion", $mvcVersion)
        $updated = 1
      }
    }
  }
}

if ($updated -eq 0) { # couldn't find the mvc binding redirect, insert it instead
  $dependentAssemblyNode = $xml.CreateElement("dependentAssembly")
  $dependentAssemblyNode.InnerXml = "<assemblyIdentity name=`"System.Web.Mvc`" publicKeyToken=`"31BF3856AD364E35`" culture=`"neutral`" /><bindingRedirect oldVersion=`"0.0.0.0-$mvcVersion`" newVersion=`"$mvcVersion`" />"
  $assemblyNode.AppendChild($dependentAssemblyNode)
}

$xml.Save($localPath)

