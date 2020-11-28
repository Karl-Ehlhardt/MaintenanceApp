# Eleven Fifty Academy Blue Badge Final Project

## Machine Maintenance App

Please visit our [Trello planning board.](https://trello.com/b/9VHzjxGe/machine-maintenance "Machine Maintenance on Trello")

This is a 4-tier WebAPI (Data, Models, Services, and Controllers) to manage machine maintenance in a production facility.  Our teammate Karl has experience in this area and presented the idea to us on planning day.  This is a business need where ever machines are used, ensuring that in our Blue Badge project we would learn to solve some "real world" problems.

Our objects include buildings, areas in buildings, machines in areas, machine tasks, and people, in two different user roles, Admins and Users.

Buildings, areas, and machines are fairly simple relational databases with joining, but machine tasks get a bit more complicated.  A machine task has a Maintenance Task Interval, and rather than creating a new task for each machine at every interval, there is a method that creates Tasks For Machine, scheduling the tasks for each machine at exactly the interval needed.  This method would eventually, in a business context, be run at the same time each day with no Admin input needed.

In our MVP, Admins have the following CRUD priveleges:

* Machines
* Areas
* Machines
* Tasks
* Create All upcoming Tasks
* Assign a user to those tasks

Users have the ability to see tasks that are not assigned, tasks that are assigned to them, or to update a machine once they have performed the maintenance.  This last operation automatically gets their ApplicationUserId.

Be setting up user roles, we were able to ensure that users cannot remove or pass off a task that has been assigned to them.

As an extension of our MVP, we also discussed: 

* Adding a Campus object to go above building, for larger manufacturing or testing businesses.
* Adding a Human Resources level that could set User Roles 
* Adding Active or Inactive states to employees and machines that would validate Admin's input. 


 



