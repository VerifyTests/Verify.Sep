# <img src="/src/icon.png" height="30px"> Verify.Sep

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/580iqf2cw0mxnqyu?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-Sep)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Sep.svg)](https://www.nuget.org/packages/Verify.Sep/)


Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of CSVs via [Sep](https://github.com/nietras/Sep).<!-- singleLineInclude: intro. path: /docs/intro.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Sep) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.Sep/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Sep)<!-- endInclude -->


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
<sup><a href='/src/Tests/Samples.cs#L30-L39' title='Snippet source file'>snippet source</a> | <a href='#snippet-VerifyCsvStream' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### IgnoreColumns

Excludes the column from the output.

<!-- snippet: IgnoreColumns -->
<a id='snippet-IgnoreColumns'></a>
```cs
[Test]
public Task IgnoreColumns() =>
    VerifyFile("sample.csv")
        .IgnoreCsvColumns("Customer Id");
```
<sup><a href='/src/Tests/Samples.cs#L12-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-IgnoreColumns' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.IgnoreColumns.verified.csv -->
<a id='snippet-Samples.IgnoreColumns.verified.csv'></a>
```csv
Index,First Name,Last Name,Company,City,Country,Phone,Dob
1,Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,2025-01-02
2,Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,2025-01-03
3,Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,2025-01-04
4,Linda,Olsen,"Dominguez, Mcmillan and Donovan",Bensonview,Dominican Republic,001-808-617-6467,2025-01-05
5,Joanna,Bender,"Martin, Lang and Andrade",West Priscilla,Slovakia (Slovak Republic),001-234-203-0635,2025-01-06
6,Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,2025-01-07
7,Darren,Peck,"Lester, Woodard and Mitchell",Lake Ana,Pitcairn Islands,(496)452-6181,2025-01-08
8,Brett,Mullen,"Sanford, Davenport and Giles",Kimport,Bulgaria,001-583-352-7197,2025-01-09
9,Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,2025-01-10
```
<sup><a href='/src/Tests/Samples.IgnoreColumns.verified.csv#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.IgnoreColumns.verified.csv' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### ScrubColumns

Replaces the column with `{Scrubbed}`.

<!-- snippet: ScrubColumns -->
<a id='snippet-ScrubColumns'></a>
```cs
[Test]
public Task ScrubCsvColumns() =>
    VerifyFile("sample.csv")
        .ScrubCsvColumns("Customer Id");
```
<sup><a href='/src/Tests/Samples.cs#L21-L28' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScrubColumns' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.ScrubCsvColumns.verified.csv -->
<a id='snippet-Samples.ScrubCsvColumns.verified.csv'></a>
```csv
Index,Customer Id,First Name,Last Name,Company,City,Country,Phone,Dob
1,{Scrubbed},Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,2025-01-02
2,{Scrubbed},Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,2025-01-03
3,{Scrubbed},Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,2025-01-04
4,{Scrubbed},Linda,Olsen,"Dominguez, Mcmillan and Donovan",Bensonview,Dominican Republic,001-808-617-6467,2025-01-05
5,{Scrubbed},Joanna,Bender,"Martin, Lang and Andrade",West Priscilla,Slovakia (Slovak Republic),001-234-203-0635,2025-01-06
6,{Scrubbed},Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,2025-01-07
7,{Scrubbed},Darren,Peck,"Lester, Woodard and Mitchell",Lake Ana,Pitcairn Islands,(496)452-6181,2025-01-08
8,{Scrubbed},Brett,Mullen,"Sanford, Davenport and Giles",Kimport,Bulgaria,001-583-352-7197,2025-01-09
9,{Scrubbed},Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,2025-01-10
```
<sup><a href='/src/Tests/Samples.ScrubCsvColumns.verified.csv#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.ScrubCsvColumns.verified.csv' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Result

[Samples.VerifyCsv#01.verified.png](/src/Tests/Samples.VerifyCsv%2300.verified.csv):


## Icon

[Csv](https://thenounproject.com/icon/csv-5776732/) designed by [iconade](https://thenounproject.com/creator/iconade3/) from [The Noun Project](https://thenounproject.com/).
