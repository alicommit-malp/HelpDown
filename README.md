# MarkDown based GUI Help system

Did you ever wondered to make a help system for your "Graphical User Interface" ? the ones 
that when the user clicks on the "?" icon on any page of your GUI a nice help or tutorial will magically 
be presented to them, if yes, then you can stop wondering :) HelpDown is here for you :)  

All resources are available at [alicommit-malp/HelpDown](https://github.com/alicommit-malp/HelpDown)

## Usage

- Add it to your project from [Nuget](https://www.nuget.org/packages/HelpDown/1.0.0)

```
dotnet add package HelpDown
```

- In Startup.cs 

```c#
public void ConfigureServices(IServiceCollection services){
    //...
    services.AddHelpDown();
    //...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env){
    //...
    app.UseHelpDown();
    //...
}
```

For instance, if you wish to make a help page for Home/Index 
- under the wwwroot folder create a new directory called "helpDown"

The folder structure will be look like this

```
+ Project Root
    wwwroot
        helpDown 
            Home
                Index
                    en.md
                    de.md
                    fr.md
                    cat.jpg
```

- Write your help files in [MarkDown](https://en.wikipedia.org/wiki/Markdown) 
> To Write your MarkDown help file, depending on your ide 
> - [Visual Studio](https://marketplace.visualstudio.com/items?itemName=cschleiden.markdown) 
> - [Visual Studio Code](https://code.visualstudio.com/Docs/languages/markdown)
> - [Rider](https://plugins.jetbrains.com/plugin/7793-markdown/)
> - [Github](https://guides.github.com/features/mastering-markdown/)

for example the content of the "en.md" will be something like this 

```markdown
# Title 
This is help document for **Index** action of **Home page**  
show the cat image which is located just beside the MD files 
![Local Image](cat.jpg) 
or show the cat image from an external link 
![External Image](https://github.com/alicommit-malp/HelpDown/raw/master/samples/wwwroot/helpDown/Home/Index/cat.jpg)
 
```

- In your Index.cshtml 

```html
@using HelpDown
<div style="text-align: left">

    @{
        if (HelpDown.Exists(ViewContext, CultureInfo.CurrentCulture))
        {
            @Html.Raw(HelpDown.GenerateHtml(ViewContext, CultureInfo.CurrentCulture))
        }
    }
</div>
```

you can add a help button and set its visibility according to 

```c#
if(HelpDown.Exists(ViewContext, CultureInfo.CurrentCulture)){
    //Help in the users current culture exists 
    //OR
    //If the the help in current culture does not exits, Help file in 
    //default culture exists
}
else{
    //Help in current culture or default current culture does not exists
}
```

### Settings
- To change the name of the "helpDown" directory

```c# 
app.UseHelpDown(folderName:"customFolderName");
```

- To change the default language (By default English is the default)

```c#
app.UseHelpDown(defaultLanguage:"de");
```

There you have it, a simple to use and manage Markdown-based help system for any ASP.Net Core
application.

All resources are available at [alicommit-malp/HelpDown](https://github.com/alicommit-malp/HelpDown)

Happy coding :)