# Eleven Fifty Academy Blue Badge Final Project

## Machine Maintenance App

### Team Bashful:

* Karl Ehlhardt
* Sam Reskala Eguiarte
* Dave Sprinkle

Please visit our [Trello planning board.](https://trello.com/b/9VHzjxGe/machine-maintenance "Machine Maintenance on Trello")

### Team and Project Structure

This is a *4-tier* WebAPI (Data, Models, Services, and Controllers) to manage machine maintenance in a testing, lab, or production facility. Our teammate Karl has experience in this area and presented the idea to us on planning day.  This is a business need where ever machines are used, ensuring that in our Blue Badge project we would learn to solve some "real world" problems.  

Our project was developed using *Agile methodology*, first by agreeing to naming principles and a data structure, then by *team writing User Stories*, *Creating Tickets* from those User Stories, *working those tickets*, and *testing* the work done on the tickets. 

Our objects include buildings, areas in buildings, machines in areas, tasks, and scheduled tasks on each machine in a simple relational database with joining tables.  Each level offers a foreign key to the next level up, i.e. area has a BuildingId property, each machine has an AreaId.  Tasks on each machine are a bit more complicated, A machine's task has an interval, and rather than manually creating a new task for each machine at every interval, there is a method that creates Tasks For Machines, scheduling the tasks for each machine at exactly the interval needed.  This method would eventually, in a business context, be run authomatically at the same time each day with no Admin input needed.

We also use our ApplicationUsers and UserRoles tables and permissions to place people in two different user roles at the time of registration--Admins and Users.

### MVP

In our MVP, Admins have the following CRUD priveleges:

* Buildings
* Areas
* Machines
* Tasks
* Create All upcoming Tasks
* Assign a user to those tasks

In order to avoid having users reassign, delay, or delete tasks assigned to them, have only the following privileges:

* See tasks assigned to them
* See unassigned tasks
* Update a task as completed

### Next steps

As an extension of our MVP, we also discussed: 

* Adding a Campus object to go above building, for larger manufacturing or testing businesses.
* Adding a Human Resources level that could set User Roles 
* Adding Active or Inactive states to employees and machines that would validate Admin's input. 


 



