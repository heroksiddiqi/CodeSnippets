using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

            namespace WebApplication1.Models
            {
    

                public class Category
                {
                    [Key]
                    public int Id { get; set; }
                    [Required]
                    public string Name { get; set; }
					
					// Start
					
                    // Changes the Label While keeping the DB column name same
                    [DisplayName("Display Order")]
                    // Limits the range. 3rd parameter dispays Error Message
                    [Range(1,100,ErrorMessage = "Range must be within 1 to 100")]
                    public int DisplayOrder { get; set; }
					
					// End
					
                    public DateTime CreatedDateTime { get; set; }
                }

    
            }
