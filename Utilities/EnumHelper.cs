using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Utilities
{
    public class EnumHelper
    {
        /// <summary>
        /// Gets the display name value for an enum
        /// </summary>
        /// <param name="value">enum value to get name for</param>
        /// <returns>the name as a string</returns>
        public static string GetDisplayValue(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        /// <summary>
        /// Create a select list for an enum, using the display name option, to be used in a dropdown menu
        /// </summary>
        /// <param name="tEnum">enum value to create select list from</param>
        /// <returns>A select list with the display name</returns>
        public static SelectList GetEnumSelectListName(Enum tEnum)
        {
            var values = from Enum item in Enum.GetValues(tEnum.GetType())
                           select new { ID = item, Name = GetDisplayValue(item) };
            return new SelectList(values, "ID", "Name");
        }

        /// <summary>
        /// Create a select list for an enum, using the display name option, to be used in a dropdown menu
        /// And select the default item
        /// </summary>
        /// <param name="tEnum">enum value to create select list from</param>
        /// <param name="id">Default value to be selected</param>
        /// <returns>A select list with the display name</returns>
        public static SelectList GetEnumSelectListName(Enum tEnum, int id)
        {
            int i = 1;
            var values = from Enum item in Enum.GetValues(tEnum.GetType())
                         select new { ID = i++, Name = GetDisplayValue(item) };
            SelectList list = new SelectList(values, "ID", "Name");
            var selected = list.Where(j => j.Value == id.ToString()).First();
            selected.Selected = true;
            return list;
        }
    }
}
