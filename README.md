# Web.API-SmartHRM
**INSTALLATION AND CONFIGURATION INSTRUCTIONS**

**PROJECT : HUMAN RESOURCE MANAGEMENT - SmartHRM**

**I. Download about code source and submission record pandemic**

\- Ma source Have can take passed 2 \_ way

\+ 	Method 1: solve Compress the attached zip file included

\+ 	Method 2: Clone via github : <https://github.com/hugnt/Web.API-SmartHRM>

\- After When load about wall labour will see attend judgment Have structure bamboo letters item like after

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.001.png)

\- The labour tool support support catch tie :

\+ IDE: Visual Studio (2022)

\+ framework : .NET 6.0, ASP.NET Core WEB API, ASP.NET Core MVC

**II. Prize prefer structure bamboo letters item** 

Attend judgment Okay develop declare according to tissue API image . That is To be will exist on both server (API) and client (UI) sides .

- API: API will Okay develop declare main above letters item **SmartHRM.API** , ex go out letters item This will link to The Project Library is the letters item : **SmartHRM.Repository , SmartHRM.Services**
- UI: UI will Okay develop declare above letters item **SmartHRM.Admin**
- The letters item still re : **SmartHRM.Lib** contain the letters hospital (. dll ) uniform service give the servives in API, **SmartHRM.Database** letters item This only contain image image for database ( no image enjoy next active dynamic belong to attend judgment )

**III. Install put chapter submit**

Structure image chapter submit will only Right real presently beside API side

- Step1: Open attend API project equals way enter letters item **SmartHRM.API .** After there open chapter submit equal how to click **SmartHRM.API.sln**

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.002.png)

- Step2: After When open let's check check see in External Solution Explorer section the exist in belong to **SmartHRM.API** then Have Add 2 project libraries **SmartHRM.Repository , SmartHRM.Services** 

  ![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.003.png)

- If Are not must be \_ add these 2 projects enter equal right clicking \_ mouse Go to Solution and then Then Add 2 missing library projects enter

  ![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.004.png)![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.005.png)( ***Attention \_*** select file: . csproj to more enter )

- Step 3: Structure image connectionString to conclude connect database, enter appsettings.json find arrive ConnectionStrings , after there replace change Chain conclude connect belong to DefaultConnections

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.006.png)

***Attention :*** let's brave tell that before there Satisfied has database of attend judge and take correct connectionString for that database .

- Step 4: Run chapter Process : India IDE 's Start button or run cmd : dotnet watch run to run chapter virgin . Pay attention times run head fairy will long because need install put the letters institute . If wall Console work will Are not newspaper error and will Have deliver Swagger interface shows town

  ![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.007.png)

- If bag error Friend need check check the item bag error see newspaper error need additional mail \_ institute come on , okay after Then we add them \_ equal way :

\+ If letters institute by Microsoft : added above Nuget Package ( yes can letters institute lack is Entity Framework Core – select version 6.0.1)

`	`+ If newspaper error letters institute generation system : need to add equal way more letters institute 	in **SmartHRM.Lib**


**IV. Run chapter submit**

***\* Attention :** **Let's brave Always protect the API Okay run in throughout too submit active dynamic of the web***

Step1: Open deliver face belong to truong submit in folder **SmartHRM.Admin**

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.008.png)

After When Open the IDE will show town like after

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.009.png)



Step 2: Run Chapter virgin Admin( UI) equals way press Start button , and delivery face head fairy collect Okay will To be image beside below

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.010.png)

`	`Step 3: Post import go to the web with talent clause

- Username: hugnt123
- Password: hung123456

After When post import wall labour deliver face will transfer arrive Dashboard page

![](Aspose.Words.ebac2bc5-2758-4591-b443-326310a16970.011.png)

Arrive This let's spread experience socks chief the position ability , translation service belong to generation system

If Have wonder infected about job install set or run chapter submit

let's contact Contact : 0946928815 ( Mr.Hung ) to Okay prize replied


**WISH YOU HAVE A GOOD EXPERIENCE WITH SmartHRM <3**

