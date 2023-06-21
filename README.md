# hms (Hotel Management System)
A minimal API powering the operations of a Berlin hotel

**Project Setup**
- Hms.Api : The endpoints for the tasks.
- Hms.Core: Serves as the backbone to transform and present data
- Hms.Data: Provides the layer that extracts data.
NOTE: 
- I have exposed all the tasks via an api to easily verify the solution. Tasks two and three were implemented.
- Json files were renamed according to task name
- The generated excel file for task 2 is saved in the Hms.Api project.

**Running the project**

Run project as the usual .NET project, via the command line in the directory or by opening it up in visual studio. After restoring the Nuget packages, project would typically start up using the https port.

Proposal to optional question: A way to automate this would be to use an Azure function to handle the process. A Timer triggered function can be set up to run at x time and either generate this report then or pick an already generated report file and send the email. An equally workable solution can be to use a recurring hangfire job to pick this report from a remote or local location and send an email.
