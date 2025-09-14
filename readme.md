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


### Sample input

<!-- snippet: sample.csv -->
<a id='snippet-sample.csv'></a>
```csv
Index,Customer Id,First Name,Last Name,Company,City,Country,Phone,Dob
1,44e0f9d9-e56e-4626-b88f-b29fecb8c584,Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,1/02/2025
2,e58dd853-3aa0-4360-9dd1-86be558a1ad8,Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,2/02/2025
3,0de1d6ab-f7ad-455c-822e-fbe6e9b7e40a,Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,3/02/2025
4,c26c7ead-2f50-44a4-bba6-2dde0228f280,Linda,Olsen,Mcmillan and Donovan,Bensonview,Dominican Republic,001-808-617-6467,4/02/2025
5,e5f7febf-bedf-4f77-924d-472f30897928,Joanna,Bender,Lang and Andrade,West Priscilla,Slovakia,001-234-203-0635,5/02/2025
6,355a51db-8b9d-4210-bf50-b4a733080985,Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,6/02/2025
7,b9d9dc41-9fc3-49d8-a418-36229ceeba0f,Darren,Peck,Woodard and Mitchell,Lake Ana,Pitcairn Islands,(496)452-6181,7/02/2025
8,f7b4f884-0d6a-4ddb-b3ac-4235b06bf390,Brett,Mullen,Davenport and Giles,Kimport,Bulgaria,001-583-352-7197,8/02/2025
9,e2aafdb2-fb28-433a-96ad-cacac4f0daf4,Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,9/02/2025
```
<sup><a href='/src/Tests/sample.csv#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-sample.csv' title='Start of snippet'>anchor</a></sup>
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


Results in:

<!-- snippet: Samples.VerifyCsv.verified.csv -->
<a id='snippet-Samples.VerifyCsv.verified.csv'></a>
```csv
Index,Customer Id,First Name,Last Name,Company,City,Country,Phone,Dob
1,Guid_1,Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,Date_1
2,Guid_2,Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,Date_2
3,Guid_3,Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,Date_3
4,Guid_4,Linda,Olsen,Mcmillan and Donovan,Bensonview,Dominican Republic,001-808-617-6467,Date_4
5,Guid_5,Joanna,Bender,Lang and Andrade,West Priscilla,Slovakia,001-234-203-0635,Date_5
6,Guid_6,Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,Date_6
7,Guid_7,Darren,Peck,Woodard and Mitchell,Lake Ana,Pitcairn Islands,(496)452-6181,Date_7
8,Guid_8,Brett,Mullen,Davenport and Giles,Kimport,Bulgaria,001-583-352-7197,Date_8
9,Guid_9,Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,Date_9
```
<sup><a href='/src/Tests/Samples.VerifyCsv.verified.csv#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.VerifyCsv.verified.csv' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Note that Guid and date scrubbing is respected.


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
1,Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,Date_1
2,Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,Date_2
3,Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,Date_3
4,Linda,Olsen,Mcmillan and Donovan,Bensonview,Dominican Republic,001-808-617-6467,Date_4
5,Joanna,Bender,Lang and Andrade,West Priscilla,Slovakia,001-234-203-0635,Date_5
6,Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,Date_6
7,Darren,Peck,Woodard and Mitchell,Lake Ana,Pitcairn Islands,(496)452-6181,Date_7
8,Brett,Mullen,Davenport and Giles,Kimport,Bulgaria,001-583-352-7197,Date_8
9,Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,Date_9
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
1,{Scrubbed},Sheryl,Baxter,Rasmussen Group,East Leonard,Chile,229.077.5154,Date_1
2,{Scrubbed},Preston,Lozano,Vega-Gentry,East Jimmychester,Djibouti,5153435776,Date_2
3,{Scrubbed},Roy,Berry,Murillo-Perry,Isabelborough,Antigua and Barbuda,-1199,Date_3
4,{Scrubbed},Linda,Olsen,Mcmillan and Donovan,Bensonview,Dominican Republic,001-808-617-6467,Date_4
5,{Scrubbed},Joanna,Bender,Lang and Andrade,West Priscilla,Slovakia,001-234-203-0635,Date_5
6,{Scrubbed},Aimee,Downs,Steele Group,Chavezborough,Bosnia and Herzegovina,(283)437-3886,Date_6
7,{Scrubbed},Darren,Peck,Woodard and Mitchell,Lake Ana,Pitcairn Islands,(496)452-6181,Date_7
8,{Scrubbed},Brett,Mullen,Davenport and Giles,Kimport,Bulgaria,001-583-352-7197,Date_8
9,{Scrubbed},Sheryl,Meyers,Browning-Simon,Robersonstad,Cyprus,854-138-4911,Date_9
```
<sup><a href='/src/Tests/Samples.ScrubCsvColumns.verified.csv#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.ScrubCsvColumns.verified.csv' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Verify a SepReader

<!-- snippet: VerifyReader -->
<a id='snippet-VerifyReader'></a>
```cs
[Test]
public Task VerifyReader()
{
    using var reader = Sep.Reader().FromFile("sample.csv");
    return Verify(reader);
}
```
<sup><a href='/src/Tests/Samples.cs#L41-L50' title='Snippet source file'>snippet source</a> | <a href='#snippet-VerifyReader' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Csv](https://thenounproject.com/icon/csv-5776732/) designed by [iconade](https://thenounproject.com/creator/iconade3/) from [The Noun Project](https://thenounproject.com/).
