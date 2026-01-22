import sys
import re
import subprocess

def get_latest_git_tag():
    try:
        tag = subprocess.check_output(
            ["git", "describe", "--tags", "--abbrev=0"],
            stderr=subprocess.DEVNULL,
            text=True
        ).strip()
        return tag.lstrip("v")
    except subprocess.CalledProcessError:
        print("Failed to get git tag")
        return None

def create_git_tag(tagName):
    try:
        subprocess.run(
            ["git", "tag", "-a", tagName, "-m", "auto publish latest versioning"],
            stderr=subprocess.DEVNULL,
            text=True
        )
        print(f"Writed new tag with name: {tagName}")
    except subprocess.CalledProcessError:
        print("Failed to create git tag")
        return None

def bump_patch(version):
    major, minor, patch = [int(x) for x in version.split(".")]

    patch += 1
    if patch >= 50:
        patch = 0
        minor += 1

    if minor >= 10:
        minor = 0
        major += 1

    return f"{major}.{minor}.{patch}"

def main():
    if len(sys.argv) != 2:
        print("Usage: bump_version.py <nuget_version>")
        return 1

    git_version = get_latest_git_tag()
    if not git_version:
        git_version = sys.argv[1]

    nuget_version = sys.argv[1]

    if git_version != nuget_version:
        print(f"Versions differ. Local: {git_version}, NuGet: {nuget_version}")
        return 0

    new_version = bump_patch(git_version)

    create_git_tag("v" + new_version)

    print(f"Version bumped: {git_version} -> {new_version}")
    return 0

if __name__ == "__main__":
    sys.exit(main())
