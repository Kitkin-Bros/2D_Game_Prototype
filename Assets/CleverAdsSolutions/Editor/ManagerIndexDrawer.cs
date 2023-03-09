﻿//
//  Clever Ads Solutions Unity Plugin
//
//  Copyright © 2023 CleverAdsSolutions. All rights reserved.
//

using UnityEngine;
using UnityEditor;
using CAS.UEditor;

namespace CAS.AdObject
{
    [CustomPropertyDrawer( typeof( ManagerIndex ) )]
    [CanEditMultipleObjects]
    internal class ManagerIndexDrawer : PropertyDrawer
    {
        private string[] androidIDs;
        private string[] iosIDs;

        public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
        {
            if (androidIDs == null)
                androidIDs = GetIDS( BuildTarget.Android );
            if (iosIDs == null)
                iosIDs = GetIDS( BuildTarget.iOS );
            var android = property.FindPropertyRelative( "android" );
            var ios = property.FindPropertyRelative( "ios" );
            position.yMax -= position.height * 0.66f;
            EditorGUI.LabelField( position, "Mediaiton manager" );
            position.y += position.height;
            EditorGUI.indentLevel++;
            android.intValue = EditorGUI.Popup( position, "Android", android.intValue, androidIDs );
            position.y += position.height;
            ios.intValue = EditorGUI.Popup( position, "iOS", ios.intValue, iosIDs );
            EditorGUI.indentLevel--;
        }

        public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
        {
            return EditorGUIUtility.singleLineHeight * 3.0f + 3.0f;
        }

        private string[] GetIDS( BuildTarget target )
        {
            var settings = CASEditorUtils.GetSettingsAsset( target, false );
            if (settings && settings.managersCount > 0)
            {
                var result = new string[settings.managersCount];
                for (int i = 0; i < settings.managersCount; i++)
                    result[i] = settings.GetManagerId( i );
                if (result.Length > 1 || !string.IsNullOrEmpty( result[0] ))
                    return result;
            }
            return new[] { "Default" };
        }
    }
}