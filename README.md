# BnGameEvoslashOpen
This is the open source version of Evoslash, the first Bodynodes games based on wifi motion capture sensors

Tested with:
- Unity 2022.3.16f1

Installation Notes:
- You need to download the submodules, here are the commands from the main directory "BnGameEvoslashOpen"
        
      # You are inside "BnGameEvoslashOpen"
      cd Assets/Bodynodes
      git submodule init
      git submodule sync
      git submodule update
      # body-nodes-host gets installed
      cd body-nodes-host
      # You are inside "body-nodes-host"
      git submodule init
      git submodule sync
      git submodule update
      # body-nodes-common gets installed

## What is it?
Evoslash is a First-Person Sword Fighting game powerd by Bodynodes.
With Bodynodes Sensor App you will be able to control your sword and to cut down objects and get points.
The more points you get, the harder the game becomes. Are you up for the challenge? 
You can find the the APK here: https://github.com/ManuDev9/body-nodes-sensor/tree/master/android/BodynodesSensor/release

Once installed, go to Evoslash main menu and press PLAY.


## Credits

Bodynodes is powering the system that makes the sensor and the game communicates. On top of that the sensor operates along Bodynodes specifications
Copyrights:

    // MIT License
    //
    // Copyright (c) 2019-2024 Manuel Bottini
    //
    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:

    // The above copyright notice and this permission notice shall be included in all
    // copies or substantial portions of the Software.

    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    // SOFTWARE.

Link:
https://bodynodesdev.wixsite.com/home

-------------------------------

Thanks to Microlight
MicroBar resources have been used for the health bar system and the progress bars
Link:
https://assetstore.unity.com/packages/tools/gui/microbar-animated-health-bar-239154

-------------------------------

Thanks to Reynard Droste
Japanese Zen Garden Pack resources have been used for the enviroment in the game

Link:
https://assetstore.unity.com/packages/3d/props/japanese-zen-garden-pack-69167

-------------------------------

Thank to Dustin Whirle
His code was used to implement the cut of the meshes

Copyrights:

    //    MIT License
    //    
    //    Copyright (c) 2017 Dustin Whirle
    //    
    //    My Youtube stuff: https://www.youtube.com/playlist?list=PL-sp8pM7xzbVls1NovXqwgfBQiwhTA_Ya
    //    
    //    Permission is hereby granted, free of charge, to any person obtaining a copy
    //    of this software and associated documentation files (the "Software"), to deal
    //    in the Software without restriction, including without limitation the rights
    //    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    //    copies of the Software, and to permit persons to whom the Software is
    //    furnished to do so, subject to the following conditions:
    //    
    //    The above copyright notice and this permission notice shall be included in all
    //    copies or substantial portions of the Software.
    //    
    //    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    //    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    //    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    //    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    //    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    //    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    //    SOFTWARE.

-------------------------------

Thanks to Synty Studios
Simple Sky - Cartoon assets have been used for the sky

Link:
https://assetstore.unity.com/packages/3d/environments/simple-sky-cartoon-assets-42373

-------------------------------

Thanks to Valday Team
Low Poly Nature Pack (Lite) assets have been used for the environment

Link:
https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-nature-pack-lite-40444

-------------------------------

The following assets have been created on Blender and then imported in the project by us:
- Sword
- Apple
- Blueberry
- Pot
- Dish
- Vase

If there are any issues or problems please let me know
>>>>>>> 6c98684 (First commit of the game)
