import sys
import re
import subprocess
import xml.etree.ElementTree as ET
from pathlib import Path

CSPROJ_PATH = "src/Starlight.NullLink.csproj"

def get_latest_git_tag():
    try:
        tag = subprocess.check_output(
            ["git", "describe", "--tags", "--abbrev=0"],
            check=True,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True
        ).strip()
        print("stdout:", result.stdout)
        return tag.lstrip("v")
    except subprocess.CalledProcessError:
        print("Failed to get git tag")
        print("return code:", e.returncode)
        print("stdout:", e.stdout)
        print("stderr:", e.stderr)
        return None

def create_git_tag(tagName):
    try:
        result = subprocess.run(
            ["git", "tag", "-a", tagName, "-m", "auto publish latest versioning"],
            check=True,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True
        )

        print("stdout:", result.stdout)

    except subprocess.CalledProcessError as e:
        print("return code:", e.returncode)
        print("stdout:", e.stdout)
        print("stderr:", e.stderr)

def bump_patch(version):
    major, minor, patch = [int(x) for x in version.split(".")]
    patch += 1
    return f"{major}.{minor}.{patch}"

def get_version_from_csproj(csproj_path):
    tree = ET.parse(csproj_path)
    root = tree.getroot()

    for elem in root.iter():
        if elem.tag.endswith("Version"):
            return elem.text

    return None

def update_csproj_version(csproj_path, new_version):
    tree = ET.parse(csproj_path)
    root = tree.getroot()

    for elem in root.iter():
        if elem.tag.endswith("Version"):
            old_version = elem.text
            elem.text = new_version
            tree.write(csproj_path, encoding="utf-8", xml_declaration=True)

            print(f"Updated csproj version: {old_version} -> {new_version}")
            return

    raise RuntimeError("Version tag not found in csproj")

def main():
    git_version = get_latest_git_tag()
    if git_version:
        print(f"Version from git tag: {git_version}")
        base_version = git_version
    else:
        print("Git tag not found, reading version from csproj")
        base_version = get_version_from_csproj(CSPROJ_PATH)

        if not base_version:
            print("Failed to read version from csproj")
            return 1

        print(f"Version from csproj: {base_version}")

    new_version = bump_patch(base_version)

    update_csproj_version(CSPROJ_PATH, new_version)

    create_git_tag("v" + new_version)

    print(f"Version bumped: {git_version} -> {new_version}")
    return 0

if __name__ == "__main__":
    sys.exit(main())
