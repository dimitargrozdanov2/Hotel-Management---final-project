# Hotel Management
<br>
Final project for the Telerik Alpha Academy

# Topic
The application allows Restaurant / Hotel customers and managers to log comments about the service. Customers will be able to add comments with positive/negative service feedback and view others’ feedback.

Managers can take notes (logs) about things happening on specific shift – for example unusual things, stuff related issues, TODOs, and sharing them between managers.

These logs are organized into LogBooks (they could be per Hotel, Restaurant in the hotel, the Spa in the hotel, etc.), and also can have categories/tags (like TODO, Maintenance, Events, etc.), logged for specific date and time.

# Technologies
- ASP.NET Core 2.2 
- MS SQL, Entity Framework Core
- HTML, CSS, Bootstrap, JavaScript, jQuery
- SignalR
- MS Test, Moq

# Pages and Functionality
- Public Part
The public part is visible without authentication.
This public part is where customers are able to add and view comments (log entries) with positive/negative service feedback. It is the application start page where could be any information like contacts and presentation details for the Restaurant / Hotel.

- Private Part (Managers only)
Managers have private part in the web application accessible after successful login.
In this part the managers are able to take notes (logs some data) rapidly with minimum clicks and data entry about things happening on specific shift. These notes are accessible also by the other managers (for example they need to check what happened on the previous shift).
The managers are able to select a LogBook where to post the log note. Optionally he/she can specify a category / tags while entering the data.
The managers are able to search by text entered, category and date. The Logs are ordered by date/time starting from the soonest on top. The list  auto refreshes in order to have the latest info on screen.
Customers are not able to access any information about these notes.

- Administration Part
System administrators have administrative access to the system and permissions to manage the accounts of other system administrators, managers and feedback moderators.
The moderators have access to the customer’s feedback posts and are able to filter out or censor posts that does not comply with common sense rules (rude, swearing, etc.).
The administrators are able to initialize new LogBooks and Categories/Tags for them. They can specify the access for each manager for each the LogBook.



# Screenshots
![Home Page](homepage.png)<br>
![Home Page](feedback-section.png)<br>
![Home Page](management-notes.png)<br>
![Home Page](search-by-category.png)<br>
![Home Page](all-businesses.png)<br>
![Home Page](business-logbooks.png)<br>
![Home Page](all-users.png)<br>