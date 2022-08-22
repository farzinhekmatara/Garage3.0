using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Core;
using Garage3._0.Data;
using Garage3._0.Web.Models;
using Garage3._0.Web.Models.Entities;




namespace Garage3._0.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageContext _context;
        public int nummer;
        public PersonInSession ps = new PersonInSession();

        public VehiclesController(GarageContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garageContext = _context.Vehicle.Include(v => v.VehicleType);
            return View(await garageContext.ToListAsync());
        }

        public IActionResult SearchMemberById(int memberId)
        {
            TempData["Member"] = memberId.ToString();
            return View("Create");
        }

       
        public IActionResult SearchMemberInVehicleTable(string id)
        {
            
            if (_context.Vehicle?.FirstOrDefault(v => v.Id== Int32.Parse(id)) == null)
            {
                TempData["Message"] = "You do not have any registered Vehicle.";
                return View("Create");
            }
            else if (_context.Vehicle?.FirstOrDefault(v => v.Id == Int32.Parse(id)) != null)
            {
                
                //var membership = await _context.Membership.FindAsync(ms.Id);
               var vehicle = _context.Vehicle
                    .Where(v => v.Id == Int32.Parse(id))
                .ToList();    
                return View("RegisteredVehicles",vehicle);
            }
        
            return View("RegisteredVehicles");
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create(int id)
        {
            Vehicle myVehicle = new Vehicle();
            myVehicle.Fuel = "";
            //myVehicle.VehicleTypeId =1;
            myVehicle.Wheels = 2;
            myVehicle.Brand = "";
            myVehicle.RegistrationNumber = "";
            myVehicle.Color = "";
            myVehicle.MembershipId= id;
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id","Name");
            return View("Create",myVehicle);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("VehicleTypeId,Fuel,Wheels,Brand,RegistrationNumber,Color,MembershipId")] Vehicle vehicle)
        public async Task<IActionResult> Create(int VehicleTypeId, string Fuel, int Wheels, string Brand,string RegistrationNumber, string Color, int MembershipId)
        {
            /*if (ModelState.IsValid)
            {
                if (_context.Vehicle?.FirstOrDefault(v => v.RegistrationNumber == vehicle.RegistrationNumber) != null)
                {
                    TempData["Message"] = "Licens exist";
                    ViewBag.LicenseNumberExists = true;
                    return View("Index");
                }
                else if (_context.Vehicle?.FirstOrDefault(v => v.RegistrationNumber == vehicle.RegistrationNumber) == null)
                {*/
            //var obj = TempData["PersonId"];
              



            Vehicle myVehicle=new Vehicle();
            myVehicle.Fuel = Fuel;
            myVehicle.VehicleTypeId = VehicleTypeId;
            myVehicle.Wheels = Wheels;
            myVehicle.Brand=Brand;
            myVehicle.RegistrationNumber = RegistrationNumber;
            myVehicle.Color = Color;
            myVehicle.MembershipId=MembershipId;
            TempData["Message"] = "Vehicle added";
            _context.Add(myVehicle);
                 await _context.SaveChangesAsync();
            // }
            //}
            //Select all
            var vehic = from e in _context.Vehicle select e;
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Name", "Id", myVehicle.VehicleTypeId);
            return View("Index",vehic);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return View("RegisteredVehicles");
                //return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MembershipId,VehicleTypeId,Fuel,Wheels,Brand,RegistrationNumber,Colour")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Parking vm = new Parking();
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'GarageContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return View("Receipt",vm);
            //return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_context.Vehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
