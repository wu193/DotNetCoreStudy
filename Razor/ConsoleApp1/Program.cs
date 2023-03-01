// See https://aka.ms/new-console-template for more information

string tm = "[)@06@12S0002@P28.5830-7610.1@1P28.5830-7610.1@31P@12V123456@10VCHN-SUZHOU@2P@20P@6D20230216@14D20260216@30PY@Z5a@K@16K@V314726@3S123123123123213666@Q200@20T1@1T666@2T@1Z@@004";
int firstIndex=tm.IndexOf("@3S");
int lastIndex = tm.IndexOf("@Q");
string a=tm.Substring(firstIndex+3, lastIndex - firstIndex-3);
Console.WriteLine("Hello, World!");
