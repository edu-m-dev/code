# code reference

## EF

### Starting with an existing database

004_refactoring.sln

https://www.learnentityframeworkcore.com/walkthroughs/existing-database
- getting started with ef core for a code-first, non-migration approach
- force model to change when db has been modified outside migrations

## Dapper

### dapper.contrib https://github.com/StackExchange/Dapper/tree/main/Dapper.Contrib

004_refactoring.sln

- CRUD ops with dapper

## git cheatsheet

**show all commits on a feature branch not coming from develop branch**

git log develop..feature/x --no-merges

ref https://stackoverflow.com/questions/1710894/using-git-show-all-commits-that-are-in-one-branch-but-not-the-others

**point local brach x to its remote**

git reset --hard origin/x

**remove untracked files from the working tree**

git clean -f <path>

## How-to

### [compare vscode with sls-based search](how-to/compare-vscode-with-sls-based-search.md)

end of file
