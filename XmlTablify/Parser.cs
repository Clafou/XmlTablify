using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace XmlTablify
{
	public class Parser
	{
		public static void Run(String path, List<Job> jobs, int printStatusNodeCount)
		{
            List<Scope> jobScopes = new List<Scope>(jobs.Count);

			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				using (XmlReader reader = XmlReader.Create(fs)) 
				{
                    int nodeCount = 0;
                    jobs.ForEach(x => jobScopes.Add(new Scope(x)));
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
                            case XmlNodeType.Element:

                                string tag = reader.Name;
                                for (int i = 0; i < jobScopes.Count; i++)
                                {
                                    jobScopes[i] = jobScopes[i].OpenTag(tag);
                                }

								if (reader.HasAttributes)
                                {
									for (int iAttr = 0; iAttr < reader.AttributeCount; iAttr++)
                                    {
										reader.MoveToAttribute(iAttr);
                                        string name = reader.Name;
                                        string value = reader.Value;
                                        for (int i = 0; i < jobScopes.Count; i++)
                                        {
                                            jobScopes[i] = jobScopes[i].Attribute(name, value);
                                        }
									}
									reader.MoveToElement();
								}
								if (reader.IsEmptyElement)
								{
                                    for (int i = 0; i < jobScopes.Count; i++)
                                    {
                                        jobScopes[i] = jobScopes[i].EndTag();
                                    }
								}
							break;

						case XmlNodeType.Text:

                            string text = reader.Value;
                            for (int i = 0; i < jobScopes.Count; i++)
                            {
                                    jobScopes[i] = jobScopes[i].Text(text);
                            }
							break;

                        case XmlNodeType.EndElement:

                            for (int i = 0; i < jobScopes.Count; i++)
                            {
                                jobScopes[i] = jobScopes[i].EndTag();
                            }
    						break;
						}

                        nodeCount++;
                        if (nodeCount % printStatusNodeCount == 0)
                        {
                            foreach (Job job in jobs)
                            {
                                bool blank = (job.Writer.GetAndResetWrittenRowCount() == 0);
                                Console.Write((blank)? "-" : "*");
                            }
                            Console.Write(" ");
                            Console.WriteLine(nodeCount);
                        }
					}

                    Console.WriteLine();
                    Console.WriteLine("Total rows written:");
                    foreach (Job job in jobs)
                    {
                        Console.WriteLine(String.Format("    {1}: {0}", job.Writer.TotalWrittenRowCount, job.Name));
                    }
				}
			}
		}
	}
}

