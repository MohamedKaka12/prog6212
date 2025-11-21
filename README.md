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
All users have access to a web browser.

The HR administrator is responsible for the initial onboarding of staff.

Users are familiar with standard file upload procedures.

## Constraints:
The system must adhere to the technological constraint of having to be developed by a MVC Framework.

# UML Class Diagram (Visual Paradigm, 2019)

<img width="721" height="235" alt="image" src="https://github.com/user-attachments/assets/a574e12d-ee11-4ea4-9d3f-fce1d138c86d" />

# Project Plan

<img width="888" height="493" alt="image" src="https://github.com/user-attachments/assets/c97285c4-74c6-4a19-a47f-3bfd9cd559ed" />

[Project Plan PDF](https://github.com/user-attachments/files/22389435/Untitled.spreadsheet.-.Sheet1.pdf)

# Graphical User Interface (Mynavi TechTus Vietnam, 2023)
### Due to the low mark that I have gotten in Part 1, I have decided to change the way everything is because I think I got the entire concept wrong at first. Part 2 mockups to follow:
## Login Screen

The entry point for all users, featuring a secure, minimalist card layout.

<img width="1918" height="970" alt="image" src="https://github.com/user-attachments/assets/eeb093d4-9f28-424a-9e71-c765288301aa" />

## HR Dashboard

HR can view system stats, manage user accounts and generate reports.

<img width="1918" height="968" alt="image" src="https://github.com/user-attachments/assets/2deb148d-07e6-43a3-b878-3879038efcaf" />

## HR User Management

<img width="1918" height="968" alt="image" src="https://github.com/user-attachments/assets/5f9d3a44-a097-4c83-95ab-3a6281bfc7d8" />

## HR Reports

<img width="1918" height="968" alt="image" src="https://github.com/user-attachments/assets/53c6687b-02f8-419b-9e15-df048c2a3e02" />

## Lecturer's Dashboard

Lecturers can view their claim history with clear status indicators.

<img width="1918" height="968" alt="image" src="https://github.com/user-attachments/assets/9df923b8-8370-4dd9-8094-311251b9ebea" />

## Submit New Claim

The claim submission form where rate is pre-filled and totals are auto-calculated.

<img width="1232" height="970" alt="image" src="https://github.com/user-attachments/assets/c6b51d8a-57a4-4279-934b-9efb09be9007" />

## Programme Coordinator Dashboard

There is a list of claims waiting for the approval.

<img width="1918" height="811" alt="image" src="https://github.com/user-attachments/assets/b3ddb7b2-7c12-44dd-8d0f-fb06144edb97" />

## Academic Manager Dashboard

There is a list of approved claims from the Programme Coordinator that are waiting for the final approval.

<img width="1918" height="867" alt="image" src="https://github.com/user-attachments/assets/907a9fc3-d6a8-4eb3-9617-196d370c0cda" />

# Reference List

Laoyan, S. (2025). What is agile methodology? (A beginner’s guide). [online] Asana. Available at: https://asana.com/resources/agile-methodology.

‌Visual Paradigm (2019). What is Unified Modeling Language (UML)? [online] Visual-paradigm.com. Available at: https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/.

‌Mynavi TechTus Vietnam (2023). 10 Principles in UI Design: Enhancing User Experience through Practical Examples. [online] Medium. Available at: https://medium.com/@MynaviTechTusVietnam/10-principles-in-ui-design-enhancing-user-experience-through-practical-examples-9d519e91b515.

‌







