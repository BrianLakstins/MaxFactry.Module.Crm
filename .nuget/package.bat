rem Package the library for Nuget
copy ..\MaxFactry.Module.Crm-NF-4.5.2\bin\Release\MaxFactry.Module.Crm*.dll lib\Crm\net452\

c:\install\nuget\nuget.exe pack MaxFactry.Module.Crm.nuspec -OutputDirectory "packages" -IncludeReferencedProjects -properties Configuration=Release 

copy ..\MaxFactry.Module.Cms.Mvc4-NF-4.5.2\bin\MaxFactry.Module.Crm.Mvc4*.dll lib\Crm.Mvc4\net452\

c:\install\nuget\nuget.exe pack MaxFactry.Module.Crm.Mvc4.nuspec -OutputDirectory "packages" -IncludeReferencedProjects -properties Configuration=Release 
