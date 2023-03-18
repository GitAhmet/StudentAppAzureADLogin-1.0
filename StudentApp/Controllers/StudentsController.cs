﻿
using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Services;

namespace StudentApp.Controllers;

//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
//[Authorize]
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IService _service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public StudentsController(IService service, IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetStudent()
    {
        var students = new List<StudentResponse>();
        var result = await _service.Get();
        if (result is null)  return Ok(students);
        for (var i = 0; i < result.Count;i++)
        {
            students.Add(new StudentResponse(result[i]));
        }
        return Ok(students);
    }

    [HttpGet("GetAsId/")]
    public async Task<ActionResult<StudentResponse>> GetAsId(int id)
    {
        var result = await _service.GetAsId(id);
        if (result == null) return NotFound($"Wrong Id {id}");
        return Ok(new StudentResponse(result));
    }

    [HttpPost]
    public async Task<ActionResult<StudentResponse>> AddStudent(AddStudentRequest addStudentRequest)
    {
        var request = new AddStudentRequest();
        var student = request.ToStudent(addStudentRequest);
        var result = await _service.AddStudent(student);
        return Ok(new StudentResponse(result));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudent(int id, UpdateStudentRequest updateStudentRequest)
    {
        var request = new UpdateStudentRequest();
        var student = request.ToUpdateStudent(updateStudentRequest);
        var result = await _service.UpdateStudent(id, student);

        if (result == null)
            return NotFound($"Wrong Id {id}");
        return Ok(new StudentResponse(result));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool?>> DeleteStudent(int id)
    {
        var result = await _service.DeleteStudent(id);
        if (result == null)
            return NotFound($"False: Wrong Id {id}");
        if (result == false)
            return NotFound($"False: Deleting Id {id} is NOT successful");
        return Ok($"True: Deleting Id {id} is successful");
    }

    //UploadImage 
    [HttpPost("UploadImage")]
    //[Route("{id}")]
    public async Task<IActionResult> UploadImage(IFormFile uploadedFile/*, int id*/)
    {
        //Save image to wwwroot/image
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string path = Path.Combine(wwwRootPath + "/Image", uploadedFile.FileName);

        using var stream = new MemoryStream();
        await uploadedFile.CopyToAsync(stream);
        var byteArray = stream.ToArray();

        //Save image path to Db by using Student id...

        try
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write (byteArray); 
                };
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound("false: uploading image is not successful");
        }
        return Ok();
    }
}