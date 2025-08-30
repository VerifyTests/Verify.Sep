# <img src="/src/icon.png" height="30px"> Verify.DocNet

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/41y3vomprwgnsheq?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-DocNet)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.DocNet.svg)](https://www.nuget.org/packages/Verify.DocNet/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of documents via [DocNet](https://github.com/GowenGit/docnet).<!-- singleLineInclude: intro. path: /docs/intro.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**
Converts pdf documents to png for verification.

This library uses [SixLabors ImageSharp](https://github.com/SixLabors/ImageSharp) for png generation. For commercial application support visit [SixLabors/Pricing](https://sixlabors.com/pricing/).


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.DocNet) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.DocNet/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.DocNet)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.Sep


## Usage


### Enable Verify.Sep

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifySep.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Verify a file

<!-- snippet: VerifyCsv -->
<a id='snippet-VerifyCsv'></a>
```cs
[Test]
public Task VerifyCsv() =>
    VerifyFile("sample.csv");
```
<sup><a href='/src/Tests/Samples.cs#L4-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-VerifyCsv' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Verify a Stream

<!-- snippet: VerifyCsvStream -->
<a id='snippet-VerifyCsvStream'></a>
```cs
[Test]
public Task VerifyCsvStream()
{
    var stream = File.OpenRead("sample.csv");
    return Verify(stream, "csv");
}
```
<sup><a href='/src/Tests/Samples.cs#L28-L37' title='Snippet source file'>snippet source</a> | <a href='#snippet-VerifyCsvStream' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Result

[Samples.VerifyCsv#01.verified.png](/src/Tests/Samples.VerifyCsv%2300.verified.csv):



## Icon

[Pdf](https://thenounproject.com/term/pdf/533502/) designed by [Alfredo](https://thenounproject.com/AlfredoCreates) from [The Noun Project](https://thenounproject.com/).
