# Folder Structure
This repository contains four main types of folders, each serving a specific purpose in the project:

1. **Properties** folder: This folder contains configuration files related to the project's settings, such as launchSettings.json. There is only one Properties folder in the repository.
2. **Protos** folder: This folder is linked to an external Proto repository, which is added as a Git submodule. Instead of referencing all the Proto files from the external repository, we only reference the ones that are used in this project. There is only one Properties folder in the repository.
3. **Infrastructures** folder:  This folder contains implementation details for the project, such as database connections and other infrastructure-related code. There is only one Properties folder in the repository.
4. **Modules** folder: This folder contains various modules specific to this repository. Each module represents a separate functional unit within the project and is organized into its own subfolder. `Authentication` folder belongs to this type. 

By organizing the project into these four main folder types, we try to ensure a clean and maintainable structure that is easy to understand and navigate.