import sys
import re

PROJECT_FILE = "../Starlight.NullLink.csproj"

VERSION_REGEX = re.compile(r"<Version>(.*?)</Version>")

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

    nuget_version = sys.argv[1]

    with open(PROJECT_FILE, "r", encoding="utf-8") as f:
        content = f.read()

    match = VERSION_REGEX.search(content)
    if not match:
        print("Version tag not found in csproj")
        return 1

    local_version = match.group(1)

    if local_version != nuget_version:
        print(f"Versions differ. Local: {local_version}, NuGet: {nuget_version}")
        return 0

    new_version = bump_patch(local_version)

    new_content = VERSION_REGEX.sub(
        f"<Version>{new_version}</Version>",
        content,
        count=1
    )

    with open(PROJECT_FILE, "w", encoding="utf-8") as f:
        f.write(new_content)

    print(f"Version bumped: {local_version} -> {new_version}")
    return 0

if __name__ == "__main__":
    sys.exit(main())
