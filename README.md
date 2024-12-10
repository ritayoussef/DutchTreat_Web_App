# 📋 DutchTreat ASP.NET Core MVC Application

## 🌟 Overview

The **DutchTreat** application is an ASP.NET Core MVC web application designed to handle e-commerce-like functionalities, including a **Contact Us** page with advanced validation, data storage, and email integration. This project demonstrates the use of best practices in web development, including **Repository Design Pattern**, **Code-First Entity Framework Core**, and **reCAPTCHA integration** for enhanced security.

---

## 🎯 Features

- **📧 Contact Us Page:**
  - Secure form with front-end and back-end validations using **Data Annotations** and **reCAPTCHA**.
  - Automatically stores form submissions in a **SQL Server database**.
  - Sends emails to both the customer and administrator using **SendGrid API**.

- **📋 MVC Pattern:**
  - Uses the **Model-View-Controller** pattern for separating concerns.
  - Includes views for Shop, Contact Us, Success, and Privacy Policy.

- **📦 Repository Design Pattern:**
  - Abstracts database operations to provide a clean and maintainable codebase.

- **🌐 reCAPTCHA Integration:**
  - Protects the Contact Us form with **Google reCAPTCHA v2**.
  - Validates user input on both the **front-end** and **back-end**.

- **📊 Database and Seeding:**
  - Implements a **Code-First Entity Framework Core** approach for the database.
  - Automatically seeds initial data into the database using `DutchSeeder`.

---

## 🚀 Getting Started

### 🛠 Prerequisites

- Visual Studio 2022
- .NET 6 SDK
- SQL Server or LocalDB
- SendGrid API Key (or Gmail API for email sending)

## 📂 Setup Instructions

### 1️⃣ Configure the database connection in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSqlLocaldb;Database=DutchTreat;Trusted_Connection=true;MultipleActiveResultSets=true;Integrated Security=true;Connect Timeout=30;"
   }
```
### 2️⃣ Add SendGrid API Key

Include your SendGrid credentials in appsettings.json:

```json
"ExternalProviders": {
  "SendGrid": {
    "ApiKey": "your-sendgrid-api-key",
    "SenderEmail": "your-email@example.com",
    "SenderName": "DutchTreat Admin"
  }
}
```
###  3️⃣ Run the Application

Follow these steps to run the application:

1. **Open the Project**:
   - Open the project in **Visual Studio**.

2. **Build the Solution**:
   - Build the solution to ensure there are no errors.

3. **Start the Application**:
   - Run the application using Visual Studio's debug mode.

4. **Access the Application**:
   - Navigate to [https://localhost:5001](https://localhost:5001) in your web browser to view the application.


---

# 🗂 Views in the Application

| **View Name**     | **Description**                                                                                  |
|--------------------|--------------------------------------------------------------------------------------------------|
| **Contact Us**     | A secure form for user inquiries. Implements front-end and back-end validation with reCAPTCHA.   |
| **Success Page**   | Displays a success message after the form is submitted, along with the submission date and time. |
| **Shop**           | Displays all products available in the shop.                                                    |
| **Privacy**        | Displays the privacy policy of the website.                                                     |
| **Error**          | Handles unexpected errors in the application.                                                   |

---

# 📦 Models

## **ContactModel**
The `ContactModel` is the core of the **Contact Us** feature. It includes several fields with validation using **Data Annotations** and **Regex**:

| **Field**        | **Validation**                                                                                  |
|-------------------|------------------------------------------------------------------------------------------------|
| **FirstName**     | Required, 2–50 characters, no numbers (`^[^0-9]+$`).                                           |
| **LastName**      | Required, 2–50 characters, no numbers (`^[^0-9]+$`).                                           |
| **Email**         | Required, valid email format (`.*@.*\.\w{2,}`).                                                |
| **Phone**         | Optional, supports formats like `5144576610`, `514 457 6610`, `1 514 457 6610`.                |
| **PostalCode**    | Required, accepts Canadian postal codes (`H9X 3L9`, `H9X3L9`, etc.), stored in uppercase.      |
| **Topic**         | Required, dynamically populated dropdown.                                                     |
| **QorC**          | Required, max 300 characters, textarea input.                                                 |
| **reCAPTCHA**     | Validates human interaction using Google reCAPTCHA.                                            |

---

# 🌐 API Endpoints

| **HTTP Method**   | **Endpoint**      | **Action**                       | **Description**                                         |
|--------------------|-------------------|-----------------------------------|---------------------------------------------------------|
| **GET**           | `/contact`        | `Contact()`                      | Displays the Contact Us form.                          |
| **POST**          | `/contact`        | `ContactAsync(ContactModel)`     | Processes the form submission.                         |
| **GET**           | `/shop`           | `Shop()`                         | Displays all products in the shop.                     |
| **GET**           | `/privacy`        | `Privacy()`                      | Displays the Privacy Policy page.                      |
| **GET**           | `/`               | `Index()`                        | Redirects to the Contact Us page.                      |

---

# 🛠️ Key Components

## **Repository Design Pattern**
The application uses the **Repository Design Pattern** to:
- **Maintainability**: Simplifies code maintenance by centralizing data access logic.
- **Testability**: Makes unit testing easier by mocking repository methods.
- **Reusability**: Encourages code reuse for common database operations.

---

## **Database Seeding**
The `DutchSeeder` class is used to:
- Populate the database with initial data.
- Ensure the application has sample products and other data on first launch.

---

## **Middlewares**
SQL Server middleware is configured using `UseSqlServer()` for seamless database interactions.

---

## **reCAPTCHA**
Implements **Google reCAPTCHA v2** for protecting the Contact Us form.

- Documentation: [reCAPTCHA for Developers](https://developers.google.com/recaptcha).

---

# 📧 Email Integration

The application uses **SendGrid API** for sending emails:
- A copy of the form submission is sent to both the administrator and the customer.
- The reply-to email is set to the customer's email for direct responses.

---

# 🛡 Security Measures

- **Data Annotations**: Ensure all fields are validated on the back-end.
- **reCAPTCHA Validation**: Protects against bots with both front-end and back-end checks.
- **Regex Validations**: Prevents invalid data inputs for emails, postal codes, and more.

---

# 📚 References

- [Google reCAPTCHA Documentation](https://developers.google.com/recaptcha)
- [SendGrid API Documentation](https://sendgrid.com/docs/)
- [Bootstrap Documentation](https://getbootstrap.com/)
- [Regular Expression Language - Quick Reference](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)  
   Provides details about crafting and using regular expressions in .NET, which helped in creating the regex for validating Canadian postal codes.
- [Efficient Regex for Canadian Postal Code](https://stackoverflow.com/questions/15774555/efficient-regex-for-canadian-postal-code-function)  
   Offers an optimized regex pattern for validating Canadian postal codes, which was adapted for this project.
- [How to Apply `ToUpper` or Other Functions to Each Input](https://www.reddit.com/r/dotnet/comments/92bzad/c_how_to_apply_toupper_or_other_function_to_each/?rdt=59260)  
   This thread inspired the logic for converting postal codes to uppercase and ensuring a space is always included in the correct position.

