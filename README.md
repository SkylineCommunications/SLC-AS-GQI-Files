# SLC-GQIDS-Files
A generic GQI (Generic Query Interface) data source to get a file list from the DM server.

The data source has these input arguments:
* Path (default: C:\Skyline DataMiner\Documents\DMA_COMMON_DOCUMENTS)
* Search Pattern (default: \*.\*)
* Recursive (default: False)

The data source returns a list of files with these fields:
* File Name
* Path
* Created
* Last Modified
* Size
* Type
* Read Only

![image](https://user-images.githubusercontent.com/110403333/218708219-fa58076d-bdc7-4a22-9df5-1d492a8274e1.png)

![image](https://user-images.githubusercontent.com/110403333/218708735-027ceb48-8d5f-4ac0-bfff-f3c7afb79e21.png)
