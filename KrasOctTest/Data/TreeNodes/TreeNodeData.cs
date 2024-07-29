using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KrasOctTest.Services;
using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class TreeNodeData
{
    //private readonly NodeService _nodeService;
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public NodeType NodeType { get; set; }
    public int? ParentNodeId { get; set; }
    public bool Editable { get; set; }
    
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Patronymic { get; set; }

    public DateTime? AcceptedDate { get; set; }

    [ForeignKey("ParentNodeId")]
    public virtual TreeNodeData ParentNode { get; set; }

}
