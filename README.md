# GenHTTP Themes

The themes found in this repository can be used to style your web applications running on the [GenHTTP webserver](https://genhttp.org).

> [!IMPORTANT]  
> GenHTTP will remove all server side rendering functionalities in version 9. See [this issue](https://github.com/Kaliumhexacyanoferrat/GenHTTP/issues/496) for more details.

## Using Themes

All of the themes in this repository are available via nuget. After installing the theme you would like to use, configure and pass a theme instance to the [Website](https://genhttp.org/documentation/content/websites) builder.

```
var theme = Theme.Create()
                 .Title("Website Title");

var website = Website.Create()
                     .Content(...)
                     .Theme(theme);
```

## Available Themes

The following themes are available in this repository.

| Theme        | Package | Preview           | Credits |
| - |-| -| -|
| [AdminLTE](https://github.com/Kaliumhexacyanoferrat/GenHTTP.Themes/tree/master/AdminLTE)      | [![nuget Package](https://img.shields.io/nuget/v/GenHTTP.Themes.AdminLTE.svg)](https://www.nuget.org/packages/GenHTTP.Themes.AdminLTE/)  |![AdminLTE](https://raw.githubusercontent.com/Kaliumhexacyanoferrat/GenHTTP.Themes/master/Screenshots/admin-lte.png) | [AdminLTE.io](https://adminlte.io/) |
| [Arcana](https://github.com/Kaliumhexacyanoferrat/GenHTTP.Themes/tree/master/Arcana)      | [![nuget Package](https://img.shields.io/nuget/v/GenHTTP.Themes.Arcana.svg)](https://www.nuget.org/packages/GenHTTP.Themes.Arcana/)  | ![Arcana](https://raw.githubusercontent.com/Kaliumhexacyanoferrat/GenHTTP.Themes/master/Screenshots/arcana.png) | [HTML5 UP](https://html5up.net/arcana) |
| [Dimension](https://github.com/Kaliumhexacyanoferrat/GenHTTP.Themes/tree/master/Dimension)      | [![nuget Package](https://img.shields.io/nuget/v/GenHTTP.Themes.Dimension.svg)](https://www.nuget.org/packages/GenHTTP.Themes.Dimension/)  | ![Dimension](https://raw.githubusercontent.com/Kaliumhexacyanoferrat/GenHTTP.Themes/master/Screenshots/dimension.png) | [HTML5 UP](https://html5up.net/dimension) |
| [Lorahost](https://github.com/Kaliumhexacyanoferrat/GenHTTP.Themes/tree/master/Lorahost)      | [![nuget Package](https://img.shields.io/nuget/v/GenHTTP.Themes.Lorahost.svg)](https://www.nuget.org/packages/GenHTTP.Themes.Lorahost/)  | ![Lorahost](https://raw.githubusercontent.com/Kaliumhexacyanoferrat/GenHTTP.Themes/master/Screenshots/lorahost.png) | [colorlib.](https://colorlib.com/wp/template/lorahost/) |
| [NoTheme](https://github.com/Kaliumhexacyanoferrat/GenHTTP.Themes/tree/master/NoTheme)      | [![nuget Package](https://img.shields.io/nuget/v/GenHTTP.Themes.NoTheme.svg)](https://www.nuget.org/packages/GenHTTP.Themes.NoTheme/)  | ![NoTheme](https://raw.githubusercontent.com/Kaliumhexacyanoferrat/GenHTTP.Themes/master/Screenshots/no-theme.png) | - |

## Building from Source

To build this project, install the [.NET 7 SDK](https://dotnet.microsoft.com/download) and run the following commands in the checked out repository root:

```
cd Demo
dotnet run
```

This launches the demo application and makes it available via http://localhost:8080/.
