## compare vscode with sls-based search
    
### ref
https://stackoverflow.com/questions/59229488/using-powershell-sls-select-string-vs-grep-vs-findstr 
https://stackoverflow.com/questions/29889495/count-specific-string-in-text-file-using-powershell 
https://ss64.com/ps/select-string.html 
AllMatches (1st in line vs all in line) - https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/select-string?view=powershell-7.2 
https://stackoverflow.com/questions/59082413/visual-studio-code-advanced-search-wildcard-in-files-to-include 
https://jpearson.blog/2020/04/08/tip-case-insensitive-glob-patterns/ 
https://devblogs.microsoft.com/powershell/how-does-select-string-work-with-pipelines-of-objects/ 

### solution

  1. vscode
    
    vscode search pattern:
    ^2022-10-19 (08|09|1[0-4]).*text of interest
    
    vscode include files:
    **/*PROD#*.log

  2. sls

    all lines including pattern: 
    sls -path *PROD#*.log -pattern "^2022-10-19 (08|09|1[0-4]).*text of interest" 
    
    all matches count: 
    (sls -path *PROD#*.log -pattern "^2022-10-19 (08|09|1[0-4]).*text of interest" -allMatches).Matches.Count

### additional work

install grep for windows, use it to do search and count
