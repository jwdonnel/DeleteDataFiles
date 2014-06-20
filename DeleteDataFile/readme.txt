John Donnelly - 6/19/2014

To use this program, simply enter in the information in the settings.xml file and run this. 
If you do not have an settings.xml file, you can run the program and select option 1.


The settings.xml should be in this format below:
<Settings>
    <FolderFileList>
        <FolderInfo>
            <Folder>DIRECTORY</Folder>
            <Files>
                <File>SOME FILE</File>
                <File>SOME FILE</File>
            </Files>
        </FolderInfo>
    </FolderFileList>
    <CheckInterval>1</CheckInterval>
</Settings>


HELP:
 - If you are getting an "Access Denied" error when trying to delete, exit the program and run it again in Administrator Mode.
 - The CheckInterval setting in the settings.xml must be greater than 1 and in seconds. (Default value is 1)
 - This cannot delete a whole drive such as C:\\. Must be a folder within that drive.


The code and program are free to use as you wish.
If you want to contact me, my email is jwdonnel@gmail.com. You can also visit me at my website http://my4dashboards.com.