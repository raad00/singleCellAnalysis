﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using phiClustCore.Interface;
using phiClustCore.Profiles;

namespace phiClustCore
{
   [Serializable]
    public class HierarchicalCInput:BaseCInput,IAutomaticProfiles
    {
           [Description("K-means algorithm: Value of DB index")]
           public float indexDB=1.4f;
           [Description("Hierarchical K-means algorithm: Minimal number of structures in the node, below that number noew nodes will")]
           public int numberOfStruct=40;
           [Description("FastDendrog algorithm: number of nodes generated by K-means. For found nodes regular dendrog is used")]
           public int numInitNodes=20;
           [Description("Hierarchical K-means algorithm: How many times K-means, for each of the level,  will be repeated")]
           public int repeatTime=4;
           [Description("Hierarchical K-means algorithm: from 2 to this number all k-NN will be checked to find best indexDB")]
           public int maxK=6;
           [Description("Needed for aglomerative clustering, defines type of linkage")]
           public AglomerativeType linkageType = AglomerativeType.AVERAGE;
           [Description("Distance measures used for clustering")]
           public DistanceMeasures distance = DistanceMeasures.HAMMING;         
         //  [Description("Path to the profile for 1djury [HIERARCHICAL]")]
//           public string jury1DProfile;
           [Description("Path to the profile for 1djury for consensus [HIERARCHICAL]")]
           public string consensusProfile;
           [Description("Iterate 2-means ")]
           public bool ItMeans=false;
           [Description("Number of iterations in 2 means")]
           public int ItNum=3;
           [Description("Use hamming consensus ")]
           public bool HConsensus;
           [Description("Use 1DJury to find reference vectors in hash")]
           public bool reference1DjuryH = true;
           [Description("Use 1DJury to find reference vectors in kmeans")]
           public bool reference1DjuryKmeans;
           [Description("Use 1DJury to find reference vectors in Aglomerative")]
           public bool reference1DjuryAglom;
           [Description("Use HTree for dendrogram")]
           public ClusterAlgorithm microCluster;
           [Description("Path to the profile for 1djury hash")]
           public string jury1DProfileH="profiles/SS-SA.profiles";
                                
           //Not saved
           
           public void GenerateAutomaticProfiles(string fileName)
           {               
               ProfileTree  t = ProfileAutomatic.AnalyseProfileFile(fileName, SIMDIST.DISTANCE);
               string profileName = ProfileAutomatic.distanceProfileName;
               t.SaveProfiles(profileName);
               t = ProfileAutomatic.AnalyseProfileFile(fileName, SIMDIST.SIMILARITY);
               profileName = ProfileAutomatic.similarityProfileName;
               t.SaveProfiles(profileName);
               consensusProfile = profileName;
               jury1DProfileH = profileName;
               }

           public override string GetVitalParameters()
           {
               string outLine = "";
               
               outLine = "Algorithm: Hierarchical==";

               outLine += " Distance Measure: " + distance + "== Linkage Type: " + linkageType;
               return outLine;
           }
    }
}
