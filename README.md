# Sushi Mail Template - A Module for Mediakiwi to create easy to use mail templates with placeholders
[![NuGet version (Sushi.MailTemplate)](https://img.shields.io/nuget/v/Sushi.MailTemplate.svg?style=flat-square)](https://www.nuget.org/packages/Sushi.MailTemplate/)
[![Build status](https://dev.azure.com/supershift/Mediakiwi/_apis/build/status/Sushi.MailTemplate)](https://dev.azure.com/supershift/Mediakiwi/_build/latest?definitionId=106)
## Features
Sushi Mail Template is a NuGet library that allows you to easily create mail templates with placeholders, repeating placeholder groups and sections that can be turned on or off.
## Using Sushi Mail Template - Setup
### Installing via NuGet
The easiest way to install Sushi.MailTemplate is via [NuGet package](https://www.nuget.org/packages/Sushi.MailTemplate) to your Mediakiwi-powered solution.

In Visual Studio's [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console),
enter the following command:

    Install-Package Sushi.MailTemplate
### Register Sushi.MailTemplate
In the startup of your app, register and configure Sushi.MailTemplate:
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// add sushi micro orm            
	services.AddMicroORM(databaseConnectionString);

	// add mail templating
	services.AddSushiMailTemplate();
}
```

Also register Sushi.MicroOrm if you haven't already.
### Mediakiwi Portal
Add the following lists to your Mediakiwi portal:
* MailTemplates_List
* SendTestMail_List
* ShowMailPreview_List
#### MailTemplates_List
Make sure this list is visible.
![MailTemplates_List](/images/MailTemplates_List.png)
#### SendTestMail_List
This list doesn't have to be visible. Set the Save button label to Send, or something else that makes sense.
![SendTestMail_List](/images/SendTestMail_List.png)
![SendTestMail_List_Label](/images/SendTestMail_List_Label.png)
#### ShowMailPreview_List
This list doesn't have to be visible.
![ShowMailPreview_List](/images/ShowMailPreview_List.png)
### SQL tables
Create the SQL table, view and index needed.
* [wim_MailTemplates](/src/Scripts/wim_MailTemplates.sql)
* [vw_MailTemplates](/src/Scripts/vw_MailTemplates.sql)
* [IX_wim_MailTemplates_Identifier](/src/Scripts/Unique%20index%20on%20wim_MailTemplates.sql)
### Send Test Mail
Out of the box, the Send Test e-mail does nothing. Since this is a Module for Mail Templates, it doesn't have any logic to send e-mails. What it does do, is raise the SendPreviewEmail event. Write your own logic and hook it up to the event. You can use [MailKit](https://github.com/jstedfast/MailKit) for example.
```csharp
// hook up the Mailer to the SendPreviewEmailEventHandler.SendPreviewEmail
Sushi.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmail += OnSendPreviewEmail;
Sushi.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmailAsync += OnSendPreviewEmailAsync;
```
```csharp
private void OnSendPreviewEmail(object sender, Sushi.MailTemplate.Logic.SendPreviewEmailEventArgs e)
{
  try
  {
      var emailFrom = e.EmailFrom;
      var emailTo = e.EmailTo;
      var subject = e.Subject;
      var body = e.Body;
            
      // create the message to be send
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress (emailFrom, emailFrom));
      message.To.Add(new MailboxAddress (emailTo, emailTo));
      message.Subject = subject;
      message.Body = (new BodyBuilder { HtmlBody = body }).ToMessageBody();

      // set up the smtp client from MailKit
      using (var client = new SmtpClient()) {
        client.Connect("smtp.friends.com", 587, false);

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate("joey", "password");

        // send the message
        client.Send(message);
        client.Disconnect(true);
      }

      // return true so the list can display a notification
      e.IsSuccess = true;
  }
  catch (Exception ex)
  {
      // handle the exception and return false
      e.IsSuccess = false;
  }
}


private async Task OnSendPreviewEmailAsync(object sender, Sushi.MailTemplate.Logic.SendPreviewEmailEventArgs e)
{
  try
  {
      var emailFrom = e.EmailFrom;
      var emailTo = e.EmailTo;
      var subject = e.Subject;
      var body = e.Body;
      var templateName = e.TemplateName;

      // create the message to be send
      var message = new MimeMessage();
      message.From.Add (new MailboxAddress (emailFrom, emailFrom));
      message.To.Add (new MailboxAddress (emailTo, emailTo));
      message.Subject = subject;
      message.Body = (new BodyBuilder { HtmlBody = body }).ToMessageBody();

      // set up the smtp client from MailKit
      using (var client = new SmtpClient()) {
        await client.ConnectAsync("smtp.friends.com", 587, false);

        // Note: only needed if the SMTP server requires authentication
        await client.AuthenticateAsync("joey", "password");

        // send the message
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
      }

      // return true so the list can display a notification
      e.IsSuccess = true;
  }
  catch (Exception ex)
  {
      // handle the exception and return false
      e.IsSuccess = false;
  }
}
```
## Using Sushi Mail Template - Creating mail templates
### The overview
The mail templates in the system are displayed in this overview. It also displays the status of the mail template, whether it is published and if it has a published version.
![Mail templates](/images/MailTemplates.png)
### Detail
A Mail Template needs an Identifier, Body and Subject. All other fields are optional. The Identifier is used in the system to create unique mail templates
![Mail template](/images/MailTemplate.png)
### Placeholders, groups and sections
A placeholder looks like [PLACEHOLDER1].
A placeholder group looks like [g:REPEATER]...[/g:REPEATER]. Everything in the group can be repeated.
A section looks like [section:OPTIONAL1]...[/section:OPTIONAL1]. These can be turned off and on.
``` html
<html>
  <body>
    <p>
      [PLACEHOLDER1]
    </p>
    [g:REPEATER]
    <p>
      Repeated content: [REPEATEDPLACEHOLDER]
    </p>
    [/g:REPEATER]
    [section:OPTIONAL1]
    <p>
      Show this only if optional 1 is requested. [OPTIONAL1PLACEHOLDER]
    </p>
    [/section:OPTIONAL1]
    [section:OPTIONAL2]    
    <p>
      Show this only if optional 2 is requested. [OPTIONAL2PLACEHOLDER]
    </p>
    [/section:OPTIONAL2]
  </body>
</html>
```
### Body example
Here is an example of a body:
![Mail Body example](/images/MailTemplate_body.png)
### Mail Preview
The preview shows the placeholders and gives an indication of what the e-mail will look like.
![Mail Preview](/images/ShowMailPreview.png)
### Send Test e-mail
The send test e-mail functionality provides texboxes to fill the placeholders. If a mail template contains sections, these can be turned on or off via checkboxes.
![Send Test Mail](/images/SendTestMail.png)
## Using Sushi Mail Template - Putting it all together
Sushi Mail Templates exist to be used with placeholders to send recipients e-mails with data. To apply data to mail templates, use the following example.
```csharp
var template = Sushi.MailTemplate.MailTemplate.Fetch("SUSHI_DISHES");
var dishes = SushiDishes.FetchAll();
var customer = SushiCustomer.Fetch(1);
if (template != null)
{
    // add the customer name
    template.PlaceholderList.Add("NAME", customer.Name);

    // add the section if the customer is special
    if(customer.IsSpecial)
    {
      // Add the section for the special customer. Leaving a section out has as result that the section is removed from the e-mail.
      template.OptionalSections.Add("SPECIALCUSTOMER");
    }

    // add the group
    template.PlaceholderGroupList.Add("SPECIALDISHES");
    // loop the dishes to fill the group
    foreach(var dish in dishes.Where(x => x.IsSpecialDish))
    {
      // add a new row first
      template.PlaceholderGroupList.AddNewRow();
      // add the dish name
      template.PlaceholderGroupList.AddNewRowItem("SPECIALDISHNAME", dish.Name);  
      // add the description
      template.PlaceholderGroupList.AddNewRowItem("SPECIALDESCRIPTION", dish.Description);  
      // add the total price
      template.PlaceholderGroupList.AddNewRowItem("SPECIALTOTALPRICE", dish.TotalPrice);      
    }
    
    // add the group
    template.PlaceholderGroupList.Add("NORMALDISHES");
    // loop the dishes to fill the group
    foreach(var dish in dishes.Where(x => !x.IsSpecialDish))
    {
      // add a new row first
      template.PlaceholderGroupList.AddNewRow();
      // add the dish name
      template.PlaceholderGroupList.AddNewRowItem("NORMALDISHNAME", dish.Name);  
      // add the description
      template.PlaceholderGroupList.AddNewRowItem("NORMALDESCRIPTION", dish.Description);  
      // add the total price
      template.PlaceholderGroupList.AddNewRowItem("NORMALTOTALPRICE", dish.TotalPrice);      
    }
    
    var mail = Sushi.MailTemplate.MailTemplate.ApplyPlaceholders(template);
    
    // the email is now complete and can be send using your own implementation
}
```
