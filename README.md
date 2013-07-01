# XmlTablify

Utility for transforming XML files (particularly huge ones where XSLT would be inadequate) into tabular text files.

I wrote this tool to help a friend who has been processing huge XML files using XSLT (to filter and extract its relevant content into a text file that is then bcp'ed into a database for subsequent use), and who experienced memory issues as the input XML file grew larger. XSLT requires an in-memory representation of the entire XML document, which is problematic when the XML document is very large. This tool does the same job using a SAX approach to make it possible to create the same output while eliminating the memory constraints (and with much increased performance).

It looks for specific rows in the XML document, and captures text elements or attributes to populate columns. These captured nodes can be either ancestors or child nodes of the row nodes.

## Usage

Build the executable then run the following command:

XmlTablify <xmlFileToParse> <configFile>

A sample config files can be found under TestFiles/Config.xml.

The config file details one or more jobs (to be run concurrently as the tool runs through the XML file, each producing a text file) with XPath selectors for the table rows and the data to capture as columns.

