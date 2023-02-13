# SLC-AS-GQI-Files
A generic GQI data source to get a file list from the DM server.

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
