# mvc-spa
Example of an MVC application looking like a Single Page Application and using MVVM. It is a test, not to be used as a base for an mvc application, and it is not a Single Page Application.

This example is based on the pluralsight course [Consolidating MVC Views Using Single Page Techniques](https://app.pluralsight.com/library/courses/mvc-application-techniques/).

### Advantages
- Use a single page for all crud operations: create, read, update, delete. 
- Code reusability with MVVM for data access and validation.

### Disadvantages
- The View is returned from the HttpPost, without any redirect, so the post is called when we refresh the page.
- Difficult to use dependency injection.
- Using the _mode_ to determine the action to perform makes the code more complex when the project grows, and the problems more complicated to investigate, everything uses the same action in the controller.
