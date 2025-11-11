<p align="center">
  <img src="https://img.shields.io/github/v/release/Jackson-Wozniak/DevBank?style=for-the-badge" alt="Version"/>
  <img src="https://img.shields.io/github/license/Jackson-Wozniak/DevBank?style=for-the-badge" alt="License"/>
  <img src="https://img.shields.io/github/downloads/Jackson-Wozniak/DevBank/total?style=for-the-badge" alt="Downloads"/>
  <img src="https://img.shields.io/github/issues/Jackson-Wozniak/DevBank?style=for-the-badge" alt="Issues"/>
</p>


## :books: Table of Contents

<ol>
    <li><a href="#installation-guide">Installation Guide</a></li>
    <li><a href="#workflow">Using in Your Workflow</a></li>
    <li><a href="#roadmap">Future Roadmap</a></li>
    <li><a href="#versions">Version History</a></li>
</ol>    

<br/> 
<!-- -------------------------------------------------------------------------------------------------------------------------------------------- -->

## :computer: Installation Guide <a id="installation-guide"></a>

#### Installing the DevBank CLI

1. Download the release by going to the Github Release page for your platform and version:
   - ```https://github.com/Jackson-Wozniak/DevBank/releases/tag/v1.0.0```
2. Extract the contents of the ZIP file into a folder on your machine:
   - Windows: ```C:\Users\user\devbank```
   - Linux/macOS: ```~/devbank```
3. Add DevBank to your Path. This allows you to run DevBank command from anywhere
   - Windows:
     - Open ```Edit the system environment variables``` from Start
     - Click ```Environment Variables...```
     - In User Variables, click ```Path -> Edit -> New``` and enter the release folder path
   - Linux/macOS:
     - Add the folder to your PATH in your shell configuration (~/.bashrc or ~.zshrc):
     ```export PATH="$HOME/DevBank:$PATH"```
4. Run ```DevBank -v``` and you should see the version of your installed release


<br/> 
<!-- -------------------------------------------------------------------------------------------------------------------------------------------- -->

## :briefcase: Using in Your Workflow <a id="workflow"></a>



<br/> 
<!-- -------------------------------------------------------------------------------------------------------------------------------------------- -->

## :clipboard: Future Roadmap <a id="roadmap"></a>

The following features are possibilities for future updates:

- SQLite-based database (replacing JSON data files)
- Identifiers for each entry
- Enhanced list options
  - search by tag, id, date, date range
  - list x number of entries, sorted by most-recent date
- Delete by id
- Delete tags
- Configuration options
  - default list command return count
  - JSON vs. SQLite
  - export data files to markdown

<br/> 
<!-- -------------------------------------------------------------------------------------------------------------------------------------------- -->

## :calendar: Version History <a id="versions"></a>

- v1.0.0 [11/11/2025] - Initial release
- v1.0.1 [11/11/2025] - Fixed file path bug causing crashes
