# Zenith Platform : User Stories & Use Cases

**Version:** 1.0 | **Status:** V1 Baseline

## Actors

| Actor       | Role                                             |
| ----------- | ------------------------------------------------ |
| Applicant   | Views programmes and submits an application.     |
| Staff/Admin | Reviews applications and manages learners.       |
| Learner     | Approved applicant participating in a programme. |

## User Stories

### Applicant

* Learn about Zenith.
* View programme information.
* Submit an application online.
* Receive application confirmation.

### Staff/Admin

* Sign in securely.
* View submitted applications.
* Review applications.
* Approve or reject applications.
* Manage learner records.
* Manage programmes and cohorts.
* Record learner progress and results.

## Use Cases

| ID    | Use Case                 | Actor       |
| ----- | ------------------------ | ----------- |
| UC-01 | View Zenith Information  | Applicant   |
| UC-02 | View Programme           | Applicant   |
| UC-03 | Submit Application       | Applicant   |
| UC-04 | Receive Confirmation     | Applicant   |
| UC-05 | Sign In                  | Staff/Admin |
| UC-06 | View Applications        | Staff/Admin |
| UC-07 | View Application Details | Staff/Admin |
| UC-08 | Review Application       | Staff/Admin |
| UC-09 | Approve Application      | Staff/Admin |
| UC-10 | Reject Application       | Staff/Admin |
| UC-11 | Manage Learner           | Staff/Admin |
| UC-12 | Manage Programme/Cohort  | Staff/Admin |
| UC-13 | Record Progress          | Staff/Admin |
| UC-14 | Record Results           | Staff/Admin |

## Core Workflow

```text
Applicant
   ↓
Programme
   ↓
Application
   ↓
Staff Review
   ↓
Approve / Reject
   ↓
Learner
   ↓
Programme / Cohort
   ↓
Progress / Results
```
