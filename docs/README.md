# how-to

### EF database first

https://www.learnentityframeworkcore.com/walkthroughs/existing-database
- getting started with ef core for a code-first, non-migration approach
- force model to change when db has been modified outside migrations

### git cheatsheet

**show all commits on a feature branch not coming from develop branch**

git log develop..feature/x --no-merges

ref https://stackoverflow.com/questions/1710894/using-git-show-all-commits-that-are-in-one-branch-but-not-the-others

**point local brach x to its remote**

git reset --hard origin/x

**remove untracked files from the working tree**

git clean -f <path>

### add all projects to a solution file 
https://stackoverflow.com/questions/52017316/how-to-add-all-projects-to-a-single-solution-with-dotnet-sln
dotnet sln add (ls -r **/*.csproj)

### Directory.Packages.props
https://github.com/Webreaper/CentralisedPackageConverter - awesome global tool to centralise all nuget refs

### iis express vs-generated cert may be expired

Use [UpdateIISExpressCertificate.ps1](https://gist.github.com/camieleggermont/5b2971a96e80a658863106b21c479988#file-updateiisexpresscertificate-ps1) to update all certs within typical iis express port range.

### [![Why do we need MediatR](https://miro.medium.com/v2/resize:fit:1100/format:webp/1*AmD4-Up1jduZ6WcVpuy1Gg.jpeg)Why do we need MediatR](why-mediatr.md)

A short post explaining why MediatR is useful in .NET apps.
