using System;
using System.Collections.Generic;
using System.IO;
using Skyline.DataMiner.Analytics.GenericInterface;

[GQIMetaData(Name = "Files")]
public class MyDataSource : IGQIDataSource, IGQIInputArguments, IGQIOnInit
{
	private GQIStringArgument _path = new GQIStringArgument("Path") { IsRequired = true, DefaultValue = @"C:\Skyline DataMiner\Documents\DMA_COMMON_DOCUMENTS" };
	private GQIStringArgument _searchPattern = new GQIStringArgument("Search Pattern") { IsRequired = false, DefaultValue = "*.*" };
	private GQIBooleanArgument _recursive = new GQIBooleanArgument("Recursive") { DefaultValue = false};
	private string path;
	private string searchPattern;
	private SearchOption recursive;

	public GQIColumn[] GetColumns()
	{
		List<GQIColumn> columns = new List<GQIColumn>
		{
			new GQIStringColumn("File Name"),
			new GQIStringColumn("Path"),
			new GQIDateTimeColumn("Created"),
			new GQIDateTimeColumn("Last Modified"),
			new GQIDoubleColumn("Size"),
			new GQIStringColumn("Type"),
			new GQIBooleanColumn("Read Only"),
		};
		return columns.ToArray();
	}

	public GQIArgument[] GetInputArguments()
	{
		return new GQIArgument[] { _path, _searchPattern, _recursive };
	}

	public GQIPage GetNextPage(GetNextPageInputArgs args)
	{
		var rows = new List<GQIRow>();

		try
		{
			System.IO.DirectoryInfo dirInfo = new DirectoryInfo(path);
			System.IO.FileInfo[] fileNames = dirInfo.GetFiles(searchPattern, recursive);

			foreach (FileInfo fileInfo in fileNames)
			{
				List<GQICell> cells = new List<GQICell>
				{
					new GQICell() { Value = fileInfo.Name },
					new GQICell() { Value = fileInfo.FullName },
					new GQICell() { Value = fileInfo.CreationTime.ToUniversalTime() },
					new GQICell() { Value = fileInfo.LastWriteTime.ToUniversalTime() },
					new GQICell() { Value = Convert.ToDouble(fileInfo.Length), DisplayValue = Convert.ToDouble(fileInfo.Length) + " B" },
					new GQICell() { Value = fileInfo.Extension },
					new GQICell() { Value = fileInfo.IsReadOnly },
				};

				rows.Add(new GQIRow(cells.ToArray()));
			}
		}
		catch (Exception)
		{
			// fail gracefully
		}

		return new GQIPage(rows.ToArray())
		{
			HasNextPage = false,
		};
	}

	public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
	{
		path = args.GetArgumentValue(_path);

		searchPattern = args.GetArgumentValue(_searchPattern);

		if (args.GetArgumentValue(_recursive))
		{
			recursive = SearchOption.AllDirectories;
		}
		else
		{
			recursive = SearchOption.TopDirectoryOnly;
		}

		return new OnArgumentsProcessedOutputArgs();
	}

	public OnInitOutputArgs OnInit(OnInitInputArgs args)
	{
		return new OnInitOutputArgs();
	}
}