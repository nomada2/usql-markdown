---
services: data-lake-analytics
platforms: dotnet
author: saveenr-msft
---

# U-SQL: Markdown outputter

This U-SQL Outputter will take a rowset and emit it as a markdown document.

```
REFERENCE ASSEMBLY [master].MarkdownFormat; // This assumes you've registered the assembly into the master db
@querylog =
  SELECT * FROM ( VALUES
  ("Banana" , 300, "Image" ),
  ("Cherry" , 300, "Image" ),
  ("Durian" , 500, "Image" ),
  ("Apple" , 100, "Web" ),
  ("Fig" , 200, "Web" ),
  ("Papaya" , 200, "Web" ),
  ("Avocado" , 300, "Web" ),
  ("Cherry" , 400, "Web" ),
  ("Durian" , 500, "Web" ) )
  AS T(Query,Latency,Vertical);  
  
OUTPUT @querylog
    TO "/querylog.md"
    USING new MarkdownFormat.MarkdownOutputter(outputHeader: true, outputHeaderType: true);
```

he output file will look like this:

```
| Query string | Latency int | Vertical string |
| --- | --- | --- |
| Banana | 300 | Image |
| Cherry | 300 | Image |
| Durian | 500 | Image |
| Apple | 100 | Web |
| Fig | 200 | Web |
| Papaya | 200 | Web |
| Avocado | 300 | Web |
| Cherry | 400 | Web |
| Durian | 500 | Web |
```

Which is displayed like this:

| Query string | Latency int | Vertical string |
| --- | --- | --- |
| Banana | 300 | Image |
| Cherry | 300 | Image |
| Durian | 500 | Image |
| Apple | 100 | Web |
| Fig | 200 | Web |
| Papaya | 200 | Web |
| Avocado | 300 | Web |
| Cherry | 400 | Web |
| Durian | 500 | Web |


# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
