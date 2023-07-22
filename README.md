DeleteDataFiles
===============

Delete multiple files and folders automatically in the background each time they are created.


John Donnelly - 6/19/2014

To use this program, simply enter in the information in the settings.xml file and run this. 
If you do not have an settings.xml file, you can run the program and select option 1.

Since the program isn't signed, you will need to click on the run anyways to start it.


<b>Screen shots</b><br />
<img alt="screenshot1" src="https://github.com/jwdonnel/DeleteDataFiles/blob/master/DeleteDataFile/example1.png" />

The settings.xml should be in this format below:
<br />
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
If you want to contact me, my email is jwdonnel@gmail.com. You can also visit me at my website https://rssblocks.com.
