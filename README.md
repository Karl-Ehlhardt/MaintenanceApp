# Eleven Fifty Academy Blue Badge Final Project

## Machine Maintenance App

### Team Bashful:

* Karl Ehlhardt
* Sam Reskala Eguiarte
* Dave Sprinkle

Please visit our [Trello planning board.](https://trello.com/b/9VHzjxGe/machine-maintenance "Machine Maintenance on Trello")

Throughout our project we used references from our past project at [Eleven Fifty](https://elevenfifty.org)

We also used [Microsoft Docs](https://docs.microsoft.com/en-us/ target=blank), and [StackOverflow](https://stackoverflow.com)



### Team and Project Structure

This is a *4-tier* WebAPI (Data, Models, Services, and Controllers) to manage machine maintenance in a testing, lab, or production facility. Our teammate Karl has experience in this area and presented the idea to us on planning day.  This is a business need where ever machines are used, ensuring that in our Blue Badge project we would learn to solve some "real world" problems.  

Our project was developed using *Agile methodology* and a nearly two-week *sprint*.  We learned in an earlier team project that as soon as we decided on an API to build, we needed to agree to naming conventions and a data structure.  After that, we *team wrote our User Stories*, *Created Tickets* from those User Stories, *worked those tickets*, and *tested* the work done on the tickets through Postman. 

The "business problem" demanded the following basic objects:

* Building
* Areas
* Machines
* Tasks

Each of these are in simple relationships with each other. Each level offers a foreign key to the next level up, i.e. area has a BuildingId property, each machine has an AreaId.  

However, the next due maintence time on each machine was a bit more complicated.  Each task for a machine has an interval, or time between maintenane due time, and rather than manually creating a new task for each machine at every interval, there is a method that creates all upcoming Tasks For the Machines, scheduling the tasks for each machine at exactly the interval needed.  This method would eventually, in a business context, be run authomatically at the same time each day with no Admin input needed.

We also use our ApplicationUsers and UserRoles tables and permissions to place people in two different user roles at the time of registration--Admins and Users.

### MVP

In our MVP, Admins have the following priveleges:

* Buildings (CRUD)
* Areas (CRUD)
* Machines (CRUD)
* Tasks (CRUD)
* Create All upcoming Tasks (Create)
* Assign a user to those tasks (Update)

In order to avoid having users reassign, delay, or delete tasks assigned to them, they have only the following privileges:

* Tasks assigned to them (Read)
* Unassigned tasks (Read)
* Task completed (Update)

### Next steps

As an extension of our MVP, we also discussed: 

* Adding a Campus object to go above building, for larger manufacturing or testing businesses.
* Adding a Human Resources level that could set User Roles 
* Adding Active or Inactive states to employees and machines that would validate Admin's input. 


 



