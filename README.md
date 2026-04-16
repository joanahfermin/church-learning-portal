# Church Learning Portal
## Project Requirements

> Version 1.1 — Draft for Discussion  
> Prepared for stakeholder review

---

## Table of Contents

1. [Overview](#overview)
2. [Users](#users)
3. [Groups](#groups)
4. [Courses](#courses)
5. [Course Content](#course-content)
6. [Quizzes](#quizzes)
7. [Unlock Logic](#unlock-logic)
8. [Groups & Enrollment](#groups--enrollment)
9. [User Dashboard](#user-dashboard)
10. [Admin Panel](#admin-panel)
11. [Reports](#reports)
12. [Out of Scope](#out-of-scope)

---

## Overview

The **Church Learning Portal** is a web-based learning system for church organizations. It allows the admin to create and publish courses with videos, reading materials, and quizzes. Users are organized into groups and enrolled into courses individually. Users log in to see their assigned courses, track their progress, and complete lessons at their own pace.

---

## Users

### What Admin Can Do
- Create new user accounts
- Edit user details (name, email)
- Reset a user's password
- Activate or deactivate accounts
- Assign users to one or more groups

### User Account Fields
| Field | Description |
|---|---|
| Full Name | User's display name |
| Email | Used for login |
| Password | Set by admin, user can change |
| Status | Active or Inactive |

### Notes
- There is **no public registration** — admin creates all accounts
- A deactivated user cannot log in
- Users have no fixed role label — their identity comes from the groups they belong to

> **Example**
>
> Bro. Hernandez is a pastor and also helps with youth ministry. Admin creates one account for him and adds him to two groups: **Pastors Manila** and **Youth Ministers**. He does not need two accounts.

---

## Groups

Groups are collections of users. Admin creates and names groups freely — there are no preset categories.

### What Admin Can Do
- Create a group with any name and optional description
- Add or remove members at any time
- Assign courses to a group (so the group has a learning track)
- View all members and their course progress from the group screen

### Notes
- A user can belong to multiple groups at the same time
- Groups have no special permissions — they are used for organizing people and bulk-enrolling into courses
- Deleting a group does **not** remove individual enrollments already made

> **Example**
>
> Admin creates the following groups:
> - **Pastors Manila** — 24 members
> - **Pastors Visayas** — 18 members
> - **Youth Ministers** — 10 members
> - **Deacons Batch 2025** — 8 members
>
> Bro. Hernandez belongs to **Pastors Manila** and **Youth Ministers** at the same time.

---

## Courses

### What Admin Can Do
- Create courses with a title, description, and optional thumbnail image
- Save a course as **Draft** (not yet visible to users)
- Publish a course when it is ready
- Unpublish a course to hide it from users
- Build course content using sections and blocks

### Course States
| State | Visible to Users |
|---|---|
| Draft | No |
| Published | Yes (only to enrolled users) |

> **Example**
>
> Admin creates **"Biblical Leadership 101"** and builds the content over several days as a draft. Once all sections and quizzes are ready, admin publishes it and enrolls the **Pastors Manila** group.

---

## Course Content

A course is built from **Sections**, and each section is built from **Blocks**. Admin stacks blocks in any order they choose.

### Structure

```
Course: Biblical Leadership 101
│
├── Section 1: Introduction to Leadership
│     ├── 📝 Text Block — Welcome message and overview
│     ├── 🎬 Video Block — Intro video by the pastor
│     └── 📋 Quiz Block — Short quiz on the intro
│
├── Section 2: Servant Leadership in Scripture
│     ├── 🎬 Video Block — Main teaching video
│     ├── 📝 Text Block — Scripture references and notes
│     ├── 🎬 Video Block — Supplementary video
│     └── 📋 Quiz Block — Section quiz
│
└── Section 3: Application and Reflection
      ├── 📝 Text Block — Reflection guide
      └── 📋 Quiz Block — Final assessment
```

### Block Types

#### 📝 Text Block
Rich text content that admin writes directly in the system.

Supports:
- Bold, italic, underline
- Headings
- Bullet and numbered lists
- Blockquotes *(great for scripture passages)*
- Links

> **Example use:** Admin pastes a scripture passage, adds commentary below it, and formats key points as a bullet list.

---

#### 🎬 Video Block
Admin pastes a video URL. The video plays directly inside the course page.

- The system tracks whether the user has watched the video
- A user must watch all videos in a section before the quiz unlocks

> **Example use:** Admin uploads a sermon recording to the video server, copies the link, and pastes it into the video block.

---

#### 📋 Quiz Block
Admin selects a quiz from the quiz library. The quiz appears inline at that point in the section.

- Quiz is locked until all videos in the same section are watched
- User must pass the quiz before moving to the next section

> **Example use:** After a teaching video and reading notes, admin adds a 10-question quiz to check understanding before the user can proceed.

---

### Reordering
- Sections can be dragged to reorder within the course
- Blocks can be dragged to reorder within a section
- All blocks are optional — a section can have text only, video only, or any combination

---

## Quizzes

Quizzes are created in a **Quiz Library** and can be selected and attached to any section block. The same quiz can be reused across multiple sections or courses.

### Question Types

#### Single Choice
User picks one correct answer from a list of options.

```
Who wrote the book of Romans?

  ○ Peter
  ● Paul       ← correct
  ○ John
  ○ Luke
```

---

#### Multiple Choice
User selects all correct answers. Must get all correct and select no wrong answers to receive full marks.

```
Which of the following are fruits of the Spirit? (Select all that apply)

  ☑ Love        ← correct
  ☐ Wealth
  ☑ Peace       ← correct
  ☑ Patience    ← correct
  ☐ Wisdom
```

---

#### True / False
User picks True or False.

```
Jesus performed his first miracle at the wedding in Cana.

  ● True        ← correct
  ○ False
```

---

### Quiz Settings

Admin configures each quiz with the following settings:

| Setting | Options | Description |
|---|---|---|
| Passing score | Any % (e.g. 70%) | Minimum score to pass |
| Attempts allowed | 1, 3, or Unlimited | How many tries the user gets |
| Show correct answers | Yes / No | Show right answers after submission |
| Randomize questions | Yes / No | Shuffle question order each attempt |
| Randomize choices | Yes / No | Shuffle answer options each attempt |

### After Submission — Result Screen

```
╔══════════════════════════════════╗
║   Quiz Complete!                 ║
║                                  ║
║   Your Score:    8 / 10          ║
║   Percentage:    80%             ║
║   Passing Score: 70%             ║
║                                  ║
║   ✅  You Passed!                ║
║                                  ║
║   [Review Answers]  [Next Section→] ║
╚══════════════════════════════════╝
```

If failed:
```
╔══════════════════════════════════╗
║   Quiz Complete!                 ║
║                                  ║
║   Your Score:    5 / 10          ║
║   Percentage:    50%             ║
║   Passing Score: 70%             ║
║                                  ║
║   ❌  Not Quite                  ║
║   Attempts remaining: 2          ║
║                                  ║
║   [Review Answers]  [Try Again]  ║
╚══════════════════════════════════╝
```

### Review Answers Screen

If admin enabled "Show correct answers", user can review after submission:

```
Q1. Who wrote the book of Romans?
    Your answer:  Peter     ❌
    Correct:      Paul

Q2. Jesus performed his first miracle at the wedding in Cana.
    Your answer:  True      ✅

Q3. Which are fruits of the Spirit? (Select all that apply)
    Your answer:  Love, Peace          ❌  (missed Patience)
    Correct:      Love, Peace, Patience
```

### Not Included
- No timer
- No essay or open-ended questions
- No weighted questions (all questions are equal value)
- No leaderboard

---

## Unlock Logic

Users cannot skip ahead. Content unlocks in sequence.

| Content | Condition to Access |
|---|---|
| Text blocks | Always visible — no condition |
| Video blocks | Always playable — no condition |
| Quiz block | All videos in the **same section** must be watched first |
| Next section | All quizzes in the **current section** must be passed first |

> **Example**
>
> Section 2 has two videos and one quiz.
> - Bro. Hernandez watches Video 1 ✅
> - He tries to take the quiz → **locked**, must watch Video 2 first
> - He watches Video 2 ✅
> - Quiz unlocks → he takes it and passes ✅
> - Section 3 unlocks

---

## Groups & Enrollment

This is the main area where admin manages who takes what course.

### Assigning Courses to a Group

Admin can assign courses to a group. This defines the **learning track** for that group — it does not automatically enroll anyone yet.

> **Example**
>
> Admin assigns two courses to **Pastors Manila**:
> - Biblical Leadership 101
> - Foundations of Ministry
>
> This means these are the courses Pastors Manila is expected to complete.

---

### Group Progress Screen

This is the main screen for managing enrollment and tracking progress. Admin opens a group and sees a table:

```
Group: Pastors Manila

                     | Biblical Leadership 101 | Foundations of Ministry |
──────────────────────────────────────────────────────────────────────────
Bro. Hernandez          | ✅ Done                 | 🔵 Ongoing              |
Bro. Mendoza         | 🔵 Ongoing              | ⬜ Not Started          |
Bro. Cruz            | —  Not Enrolled         | ✅ Done                 |
Bro. Garcia          | ⬜ Not Started          | —  Not Enrolled         |
Bro. Reyes           | —  Not Enrolled         | —  Not Enrolled         |
```

From this screen admin can immediately see:
- Who is enrolled and who is not
- What progress each person has made
- Who still needs to be enrolled

---

### Progress States

| State | Meaning |
|---|---|
| — Not Enrolled | User has no access to this course yet |
| ⬜ Not Started | Enrolled but has not begun |
| 🔵 Ongoing | Has started but not yet completed |
| ✅ Done | All sections completed and all quizzes passed |

---

### Enrolling Users

Enrollment is always at the **individual level**. Each person is enrolled one by one into the course. The group is used as a convenient way to find and select people — not as a permanent link.

#### Option 1 — Enroll from Group (Batch)

Admin selects a group, sees the member list, checks who to enroll:

```
Enroll into: Biblical Leadership 101

Group: Pastors Manila

  ☑  Bro. Hernandez
  ☑  Bro. Mendoza
  ☐  Bro. Cruz          ← admin unchecks — already done
  ☑  Bro. Garcia
  ☑  Bro. Reyes

  [Select All]  [Deselect All]

  [Enroll Selected (4)]
```

#### Option 2 — Enroll Individual

Admin searches for a specific person by name and enrolls them directly.

```
Search user: [Bro. de la Cruz____________]

Results:
  + Bro. Juan de la Cruz — Pastors Visayas
```

#### Important Enrollment Rules

| Scenario | What Happens |
|---|---|
| User is already enrolled | Skipped silently, no error, no duplicate |
| Admin enrolls 5, one already enrolled | 4 enrolled, 1 skipped — confirmation shown |
| New member added to group later | **Not** automatically enrolled — admin must enroll manually |
| User removed from a group | Enrollment is **not** affected — they keep their access |
| Admin removes enrollment | User loses access, but progress and quiz scores are kept |
| User re-enrolled later | Progress resumes from where they left off |

> **Example**
>
> Admin opens **Pastors Manila** group, goes to **Biblical Leadership 101** column, sees Bro. Reyes is "Not Enrolled". Admin checks his name and clicks Enroll. Bro. Reyes now has access and his status shows "Not Started".
>
> Two weeks later, a new pastor Bro. Villanueva joins the Pastors Manila group. He does **not** automatically get enrolled in any course — admin sees him on the group progress screen as "Not Enrolled" and can enroll him when ready.

---

## User Dashboard

When a user logs in, they see their courses organized by the groups they belong to. This gives them a clear picture of what they need to complete.

### Dashboard Layout

```
Welcome back, Bro. Hernandez 👋

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📌 Pastors Manila
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  Biblical Leadership 101
  ████████░░  80% · Ongoing
  [Continue →]

  Foundations of Ministry
  ██████████  100% · Done ✅
  [Review]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📌 Youth Ministers
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  Advanced Preaching
  ░░░░░░░░░░  0% · Not Started
  [Start →]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

### Dashboard Rules
- Courses are grouped under the group they came from
- If a user was enrolled individually (not via a group), courses appear under a **"My Courses"** section
- If a user belongs to only one group, only that group is shown — no extra clutter
- Button label changes based on status: **Start**, **Continue**, or **Review**
- Completed courses stay visible so user can review anytime

---

## Admin Panel

Admin has a dedicated panel separate from the user-facing portal.

### Navigation

| Section | Purpose |
|---|---|
| Dashboard | Overview and quick stats |
| Courses | Create and manage courses and content |
| Quizzes | Create and manage the quiz library |
| Users | Manage user accounts |
| Groups | Manage groups, members, course assignments, and enrollment |
| Reports | *(To be defined later)* |

### Admin: Users Screen
- List of all users with search and filters
- Filter by group, status (active/inactive)
- Each row shows: Name, Email, Groups, Status
- Actions per user: Edit, Activate/Deactivate, Reset Password, Manage Groups

### Admin: Groups Screen
- List of all groups
- Each group shows: Name, Member Count
- Click a group to open the **Group Detail Screen**

### Admin: Group Detail Screen
- Shows group name and description
- Tab 1 — **Members**: list of members, search to add new members, remove members
- Tab 2 — **Courses**: assigned courses, progress table (members × courses), enroll actions

### Admin: Course Builder
- Add, edit, delete sections
- Within each section: add, edit, delete, reorder blocks
- Block types: Text (rich editor), Video (paste URL), Quiz (select from library)

### Admin: Quiz Builder
- Create quizzes independently of courses
- Add questions one by one
- Set question type, write question text, add choices, mark correct answer(s)
- Configure quiz settings (passing score, attempts, randomize, show answers)

---

## Reports

To be discussed and defined in a future phase after the core system is built and in use.

Likely to include: course completion rates, quiz scores per user, group progress summary.

---

## Out of Scope

The following are **not included** in this version:

| Feature | Notes |
|---|---|
| Public registration | Admin creates all accounts |
| Payment or subscriptions | Not applicable |
| Timed quizzes | No countdown timer |
| Essay / open-ended questions | Auto-grading only |
| Leaderboard or badges | Not appropriate for this context |
| Live video or live sessions | Pre-recorded only |
| Mobile app | Web only, but designed to be mobile-friendly |
| Multiple admins with different permissions | One admin level only |
