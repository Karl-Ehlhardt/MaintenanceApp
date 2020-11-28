# Eleven Fifty Academy Blue Badge Final Project

## Machine Maintenance App

### Team Bashful:

* Karl Ehlhardt
* Sam Reskala Eguiarte
* Dave Sprinkle

Please visit our [Trello planning board.](https://trello.com/b/9VHzjxGe/machine-maintenance "Machine Maintenance on Trello")

This is a 4-tier WebAPI (Data, Models, Services, and Controllers) to manage machine maintenance in a testing, lab, or production facility. Our teammate Karl has experience in this area and presented the idea to us on planning day.  This is a business need where ever machines are used, ensuring that in our Blue Badge project we would learn to solve some "real world" problems.  

It was developed using Agile methodology, first by agreeing to naming principles and a data structure, then by team writing User Stories, Creating Tickets from those user stories, working those tickets, and then moving them into a testing area. 

Our objects include buildings, areas in buildings, machines in areas, tasks, and scheduled tasks on each machine.

Buildings, areas, and machines are fairly simple relational databases with joining tables, but machine tasks get a bit more complicated.  A machine's task has an interval, and rather than manually creating a new task for each machine at every interval, there is a method that creates Tasks For Machines, scheduling the tasks for each machine at exactly the interval needed.  This method would eventually, in a business context, be run at the same time each day with no Admin input needed.

We also use our ApplicationUsers and UserRoles tables and permissions to place people in two different user roles at the time of registration--Admins and Users.

In our MVP, Admins have the following CRUD priveleges:

* Machines
* Areas
* Machines
* Tasks
* Create All upcoming Tasks
* Assign a user to those tasks

Users have the ability to see all tasks that are assigned to them, tasks that are unassigned, and to update a machine once they have performed the scheduled maintenance.  This last operation automatically gets their ApplicationUserId. Users cannot remove or pass off a task that has been assigned to them.

As an extension of our MVP, we also discussed: 

* Adding a Campus object to go above building, for larger manufacturing or testing businesses.
* Adding a Human Resources level that could set User Roles 
* Adding Active or Inactive states to employees and machines that would validate Admin's input. 


 



