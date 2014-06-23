DeleteDataFiles
===============

Delete multiple files and folders automatically in the background each time they are created.


John Donnelly - 6/19/2014

To use this program, simply enter in the information in the settings.xml file and run this. 
If you do not have an settings.xml file, you can run the program and select option 1.


<b>Screen shots</b><br />
<img alt="screenshot1" src="http://my4dashboards.com/CloudFiles/73e09d1a-6522-4332-9822-983b19e94c38.PNG" />
<br />
<img alt="screenshot2" src="http://my4dashboards.com/CloudFiles/9d4289dc-cb9c-49f1-a747-218004d5c604.PNG" />

The settings.xml should be in this format below:
&lt;Settings&gt;
<br />
    &lt;FolderFileList&gt;
    <br />
        &lt;FolderInfo&gt;
        <br />
            &lt;Folder&gt;DIRECTORY&lt;/Folder&gt;
            <br />
            &lt;Files&gt;
            <br />
                &lt;File&gt;SOME FILE&lt;/File&gt;
                <br />
                &lt;File&gt;SOME FILE&lt;/File&gt;
                <br />
            &lt;/Files&gt;
            <br />
        &lt;/FolderInfo&gt;
        <br />
    &lt;/FolderFileList&gt;
    <br />
    &lt;CheckInterval&gt;1&lt;/CheckInterval&gt;
    <br />
&lt;/Settings&gt;


HELP:
 - If you are getting an "Access Denied" error when trying to delete, exit the program and run it again in Administrator Mode.
 - The CheckInterval setting in the settings.xml must be greater than 1 and in seconds. (Default value is 1)
 - This cannot delete a whole drive such as C:\\. Must be a folder within that drive.


The code and program are free to use as you wish.
If you want to contact me, my email is jwdonnel@gmail.com. You can also visit me at my website http://my4dashboards.com.
