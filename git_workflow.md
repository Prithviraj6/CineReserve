# Development Workflow Guide

Follow these commands to keep your repository clean and your progress saved as you code.

## 1. Daily Coding Loop
As you write code, use these commands frequently to save your progress:

```powershell
# Check what files you've changed
git status

# Add all changes to the staging area
git add .

# Save your changes with a descriptive message
git commit -m "Describe what you did (e.g., 'Add user login logic')"

# Push your changes to GitHub
git push
```

## 2. Managing Branches (Best Practice)
For new features, it's best to work on a separate branch:

```powershell
# Create and switch to a new branch
git checkout -b feature/new-feature-name

# ... work and commit ...

# Switch back to main
git checkout main

# Merge your feature branch into main
git merge feature/new-feature-name

# Delete the branch after merging
git branch -d feature/new-feature-name
```

## 3. Handling Remote Changes
If you or someone else makes changes on GitHub directly (like editing the README), pull those changes down:

```powershell
# Pull the latest changes from the remote
git pull
```

## 4. Undoing Changes
If you make a mistake before committing:

```powershell
# Discard changes in a specific file
git checkout filename

# Discard ALL uncommitted changes (CAREFUL!)
git reset --hard
```

## 5. View History
```powershell
# See a log of all commits
git log --oneline --graph --all
```
