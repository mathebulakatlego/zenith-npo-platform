# Zenith Platform : Requirements

**Version:** 1.0 | **Status:** V1 Baseline | **Date:** September 2026

## Non-Functional Requirements

| ID     | Requirement                                                                     |
| ------ | ------------------------------------------------------------------------------- |
| NFR-01 | Protect authenticated and sensitive information.                                |
| NFR-02 | Handle personal information with appropriate POPIA considerations.              |
| NFR-03 | Keep the platform simple and understandable.                                    |
| NFR-04 | Provide reasonable performance for expected V1 usage.                           |
| NFR-05 | Keep frontend, backend and database responsibilities separated.                 |
| NFR-06 | Handle common errors safely and reliably.                                       |
| NFR-07 | Support semantic HTML, labels and practical keyboard accessibility.             |
| NFR-08 | Allow reasonable future growth without unnecessary V1 complexity.               |
| NFR-09 | Provide appropriate application logging without exposing sensitive information. |

## V1 Scope

### In Scope

* Public website
* Programmes and programme details
* Online application
* Application confirmation
* Authentication
* Application review
* Approve/reject applications
* Learner records
* Programme and cohort management
* Basic progress and results
* Backend API
* SQL database
* Testing
* Cloud deployment
* GitHub documentation

### Out of Scope

* Full LMS
* Payments and accounting
* HR management
* Mobile application
* Real-time chat
* Advanced analytics
* AI admissions decisions
* Microservices
* Complex multi-tenancy
* Enterprise infrastructure
* Sophisticated role hierarchy

## Acceptance Criteria

The V1 system must allow users to:

* Access and navigate the public website.
* View programme information.
* Submit an application.
* Receive application confirmation.
* Store applications in the database.
* Allow authorised staff to sign in.
* Review, approve or reject applications.
* Create and manage learner records.
* Associate learners with programmes and cohorts.
* Record basic progress and results.
* Prevent unauthorised access to protected information.
* Deploy the application to the selected cloud environment.

## Traceability

```text
Requirement → User Story → Use Case → Design → Implementation → Test
```

## V1 Baseline

```text
Public Information
        ↓
Application
        ↓
Application Review
        ↓
Approval
        ↓
Learner Management
        ↓
Basic Progress / Results
```
