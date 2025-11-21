# Documentation (Laoyan, 2025)

## Design Choices and GUI layout
For Part 3, the design changed towards a "Corporate Minimalist" aesthetic to enhance professional usability and reduce visual clutter.

Color Palette: A strict Monochrome theme (Black, Grey, Off-White) allows content to take center stage.

Functional Color: Vibrant colors are reserved only for status indicators to provide immediate visual feedback:

🟢 Green: Approved / Success

🟡 Amber/Yellow: Pending / Warning

🔴 Red: Rejected / Error

Typography: The Inter font family is used for a clean, modern, and highly readable interface suitable for financial data.

Navigation: A consistent top-navigation bar with a glass-morphism effect ensures easy access to role-specific dashboards.

## Key Features (Part 3 Updates)
### 1. HR Administration (New Role)
A new main role (HR) has been introduced to centralise control:
  
  User Management: HR creates and manages all user accounts (Lecturers, Coordinators, Managers). Public registration is restricted.
  
  Rate Management: HR sets the agreed Hourly Rate for each lecturer, preventing manual entry errors during claim submission.
  
  Reporting: A dedicated reporting view allows HR to generate claim invoices.

### 2. Streamlined Claim Submission
The Lecturer's claim process has been automated and secured:
  
  Auto-Calculation: The Hourly Rate is automatically pulled from the HR record.
  
  Validation: Strict server-side validation ensures:
    
    Claims cannot exceed 180 hours per month.
    
    Supporting documents must be valid file types (PDF, DOCX, XLSX) and under 5MB.

### 3. Approval Workflow
Programme Coordinator: Reviews claims first.

Academic Manager: Provides final approval.

Status Tracking: Lecturers can track the exact status of their claim and view rejection reasons if applicable.

## Database Structure (not used in Part 2)
The database will be designed to support the systems main features. It will have three tables:
Users Table: Manage different user roles
Claims Table: Store all claim details
Documents Table: This will link supporting files to their respective claims

## Assumptions:
All users wiill have access to a device with a web browser.

All uploaded documents will be common and in viewable formats.

User base will consist of three defined roles (Lecturer, Programme Coordinator, Academic Manager).

## Constraints:
This is a prototype and does not have a fully functional back-end.

The system must adhere to the technological constraint of having to be developed by a MVC Framework.

The submission must be version-controlled on Github with at least five documented commits.

# UML Class Diagram (Visual Paradigm, 2019)

<img width="721" height="235" alt="image" src="https://github.com/user-attachments/assets/a574e12d-ee11-4ea4-9d3f-fce1d138c86d" />

# Project Plan

<img width="888" height="493" alt="image" src="https://github.com/user-attachments/assets/c97285c4-74c6-4a19-a47f-3bfd9cd559ed" />

[Project Plan PDF](https://github.com/user-attachments/files/22389435/Untitled.spreadsheet.-.Sheet1.pdf)

# Graphical User Interface (Mynavi TechTus Vietnam, 2023)
### Due to the low mark that I have gotten in Part 1, I have decided to change the way everything is because I think I got the entire concept wrong at first. Part 2 mockups to follow:
## Login Screen

This is the first screen the user will see, if not registered, they must register.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/4e228934-a886-4c63-be51-98f0cbb8a5ee" />

If the user is registered, they must just login.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/a3ac731c-e19d-4482-be7e-15a336685e20" />

## Lecturer's Dashboard

This is the lecturers dashboard where you can basically see everything like how many claims are pending, approved or rejected with a list of all your claims and its status on the right. And a button to take you to the form.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/dc099fe8-9445-41ac-9877-3e528e5f7087" />

## Submit New Claim

This is the form that you will fill out to submit a claim with a section to upload any supporting documents.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/c37b32e1-752b-40c7-82c4-98ca73a6bd8e" />

## Programme Coordinator Dashboard

This is for the Programme Coordinator. There is a list of claims waiting for the approval. (Just need to fix the amount display)

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/066b33c1-546a-420f-8d87-d32ff802c19d" />

## Academic Manager Dashboard

This is for the Academic Manager. There is a list of approved claims from the Programme Coordinator that i waiting for a final approval.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/2b92ac34-f3f6-4e05-9e12-e094e7c65e5c" />

# Reference List

Laoyan, S. (2025). What is agile methodology? (A beginner’s guide). [online] Asana. Available at: https://asana.com/resources/agile-methodology.

‌Visual Paradigm (2019). What is Unified Modeling Language (UML)? [online] Visual-paradigm.com. Available at: https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/.

‌Mynavi TechTus Vietnam (2023). 10 Principles in UI Design: Enhancing User Experience through Practical Examples. [online] Medium. Available at: https://medium.com/@MynaviTechTusVietnam/10-principles-in-ui-design-enhancing-user-experience-through-practical-examples-9d519e91b515.

‌







