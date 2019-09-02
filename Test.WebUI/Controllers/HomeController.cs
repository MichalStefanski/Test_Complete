using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Domain.Abstract;
using Test.Domain.Entities;
using Test.WebUI.Models;

namespace Test.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerRepository customerRepository;
        private IContactRepository contactRepository;
        private ITransactionRepository transactionRepository;
        private ICouponRepository couponRepository;
        private IPrizeRepository prizeRepository;
        private ICouponClaimPrizeRepository couponClaimPrizeRepository;

        public HomeController(ICustomerRepository customerRepo, IContactRepository contactRepo, ITransactionRepository transactionRepo, ICouponRepository couponRepo, IPrizeRepository prizeRepo, ICouponClaimPrizeRepository couponClaimPrizeRepo)
        {
            customerRepository = customerRepo;
            contactRepository = contactRepo;
            transactionRepository = transactionRepo;
            couponRepository = couponRepo;
            prizeRepository = prizeRepo;
            couponClaimPrizeRepository = couponClaimPrizeRepo;
        }

        //Default page after log in - List of customers
        public ActionResult Index(string searching = "")
        {            
            return View(customerRepository.Customers.Where(x => x.PhoneNumber.ToString().Contains(searching) || searching == null).ToList());
        }

        //Display list of registerd transactions
        public ActionResult TransactionList(string searching = "")
        {
            return View(transactionRepository.Transactions.Where(x => x.TransactionNumber.Contains(searching) || searching == null).ToList());
        }

        //Connect form to customer
        public ActionResult EditCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = customerRepository.Customers.FirstOrDefault(g => g.CustomerID == id);
            Contact contact = contactRepository.Contacts.FirstOrDefault(c => c.ContactID == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            FormViewModel formViewModel = new FormViewModel
            {
                GetCustomerViewModel = new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    PhoneNumber = customer.PhoneNumber
                },

                GetContactViewModel = new ContactViewModel
                {
                    ContactID = contact.ContactID,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PostCode = contact.PostCode,
                    Email = contact.Email
                }               
            };

            return View(formViewModel);
        }

        //Modify customer data
        [HttpPost]
        public ActionResult EditCustomer(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer
                {
                    CustomerID = model.GetCustomerViewModel.CustomerID,
                    PhoneNumber = model.GetCustomerViewModel.PhoneNumber
                };

                Contact contact = new Contact
                {
                    ContactID = model.GetContactViewModel.ContactID,
                    FirstName = model.GetContactViewModel.FirstName,
                    LastName = model.GetContactViewModel.LastName,
                    PostCode = model.GetContactViewModel.PostCode,
                    Email = model.GetContactViewModel.Email
                };

                customerRepository.SaveCustomer(customer);
                contactRepository.SaveContact(contact, customer);

                TempData["message"] = string.Format("{0} is saved", model.GetCustomerViewModel.PhoneNumber);
                return RedirectToAction("Index");                
            }
            else
            {
                return View("EditCustomer", model);
            }            
        }
        
        //Prevent from creating same cusyomer twice
        [HttpPost]
        public JsonResult DuplicateCustomer(FormViewModel model)
        {
            var isEqual = customerRepository.Customers.Any(x => x.PhoneNumber == model.GetCustomerViewModel.PhoneNumber);

            if (model.GetCustomerViewModel.CustomerID == 0)
            {
                return Json(!isEqual, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isTheSame = customerRepository.Customers.FirstOrDefault(x => x.CustomerID == model.GetCustomerViewModel.CustomerID).PhoneNumber;
                if (isTheSame == model.GetCustomerViewModel.PhoneNumber)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                return Json(!isEqual, JsonRequestBehavior.AllowGet);
            }
        }

        //Use EditCustomer form to create new customer
        public ActionResult CreateCustomer()
        {
            return View("EditCustomer", new FormViewModel()
                {
                    GetCustomerViewModel = new CustomerViewModel(),
                    GetContactViewModel = new ContactViewModel()
            });
        }

        //Action to transaction edit form
        public ActionResult CouponRegister(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = transactionRepository.Transactions.FirstOrDefault(g => g.TransactionID == id);
            var cId = transaction.CustomerID;
            Customer customer = customerRepository.Customers.FirstOrDefault(c => c.CustomerID == cId);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            FormViewModel formViewModel = new FormViewModel
            {
                GetCustomerViewModel = new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    PhoneNumber = customer.PhoneNumber
                },

                GetTransactionViewModel = new TransactionViewModel
                {
                    TransactionID = transaction.TransactionID,
                    CustomerID = transaction.CustomerID,
                    TransactionNumber = transaction.TransactionNumber,
                    TransactionValue = transaction.TransactionValue,
                    CouponNumber = transaction.CouponNumber,
                    PrizeID = transaction.PrizeID,
                    TransactionDate = transaction.TransactionDate,
                    PrizeRecieved = transaction.PrizeRecieved                   
                },

                CouponViewModels = new List<CouponViewModel>(),
                GetCouponViewModel = new CouponViewModel(),
                GetPrizeViewModel = new PrizeViewModel(),

                PrizeViewModels = prizeRepository.Prizes.Select(prize => new SelectListItem { Text = prize.PrizeName, Value = prize.PrizeID.ToString() }).ToList()
            };

            foreach (var coupon in couponRepository.Coupons)
            {
                formViewModel.GetCouponViewModel.CouponID = coupon.CouponID;
                formViewModel.GetCouponViewModel.CouponNumber = coupon.CouponNumber;
                formViewModel.GetCouponViewModel.CouponOwner = coupon.CouponOwner;
                formViewModel.GetCouponViewModel.CouponUsed = coupon.CouponUsed;
                formViewModel.GetCouponViewModel.TransactionID = coupon.TransactionID;
                formViewModel.CouponViewModels.Add(formViewModel.GetCouponViewModel);
            }

            foreach (var prize in prizeRepository.Prizes)
            {
                formViewModel.GetPrizeViewModel.PrizeID = prize.PrizeID;
                formViewModel.GetPrizeViewModel.PrizeName = prize.PrizeName;
            }



            return View(formViewModel);
        }

        //Action to transaction edit form - POST
        [HttpPost]
        public ActionResult CouponRegister(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction
                {
                    TransactionID = model.GetTransactionViewModel.TransactionID,
                    CustomerID = model.GetTransactionViewModel.CustomerID,
                    PrizeID = model.GetTransactionViewModel.PrizeID,
                    CouponNumber = model.GetTransactionViewModel.CouponNumber,
                    TransactionNumber = model.GetTransactionViewModel.TransactionNumber,
                    TransactionValue = model.GetTransactionViewModel.TransactionValue,
                    TransactionDate = model.GetTransactionViewModel.TransactionDate
                };

                couponRepository.RedeemCoupon(transaction);
                transactionRepository.AddTransaction(transaction);

                TempData["message"] = string.Format("{0} is saved", model.GetTransactionViewModel.TransactionNumber);
                return RedirectToAction("TransactionList");
            }
            else
            {
                return View("CouponRegister", model);
            }
        }        

        //Prevents from assigning same coupon twice
        [HttpPost]
        public JsonResult FreshCoupon(FormViewModel model)
        {   
            var isEqual = couponRepository.Coupons.Any(x => x.CouponNumber == model.GetTransactionViewModel.CouponNumber); //true if coupon exists on list of coupons

            if (!isEqual) 
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var isInUse = couponRepository.Coupons.FirstOrDefault(x => x.CouponNumber == model.GetTransactionViewModel.CouponNumber).CouponUsed; //true if cupon already used
            
            if (isInUse == true)
            {
                int transId = 0;
                var coupon = couponRepository.Coupons.FirstOrDefault(x => x.CouponNumber == model.GetTransactionViewModel.CouponNumber).TransactionID; //checks for transaction paired to coupon
                var trans = model.GetTransactionViewModel; 
                if (trans != null)
                {
                    transId = trans.TransactionID;
                }

                if (coupon != transId) //check if used coupon is already assigned to this customer
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }      

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //Action to redeem new coupon using transaction edit form
        public ActionResult NewCouponRegister(int? id)
        {
            Customer customer = customerRepository.Customers.FirstOrDefault(c => c.CustomerID == id);
            var model = new FormViewModel()
            {
                GetTransactionViewModel = new TransactionViewModel
                {
                    PrizeID = 1
                },
                GetCustomerViewModel = new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    PhoneNumber = customer.PhoneNumber
                },
                PrizeViewModels = prizeRepository.Prizes.Select(prize => new SelectListItem { Text = prize.PrizeName, Value = prize.PrizeID.ToString() }).ToList()
            };
            return View("CouponRegister", model);
        }

        //Action confirms and retrival of prize
        public ActionResult RedeemPrize(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Transaction transaction = transactionRepository.Transactions.FirstOrDefault(t => t.TransactionID == id);
            
            if (transaction == null)
            {
                return HttpNotFound();
            }

            Customer customer = customerRepository.Customers.FirstOrDefault(q => q.CustomerID == transaction.CustomerID);
            Contact contact = contactRepository.Contacts.FirstOrDefault(c => c.ContactID == customer.CustomerID);
            Prize prize = prizeRepository.Prizes.FirstOrDefault(p => p.PrizeID == transaction.PrizeID);

            FormViewModel formViewModel = new FormViewModel
            {
                GetCustomerViewModel = new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    PhoneNumber = customer.PhoneNumber
                },
                GetContactViewModel = new ContactViewModel
                {
                    ContactID = contact.ContactID,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PostCode = contact.PostCode,
                    Email = contact.Email
                },
                GetTransactionViewModel = new TransactionViewModel
                {
                    CustomerID = transaction.CustomerID,
                    TransactionID = transaction.TransactionID,
                    CouponNumber = transaction.CouponNumber,
                    PrizeID = transaction.PrizeID,
                    PrizeRecieved = transaction.PrizeRecieved,
                    TransactionDate = transaction.TransactionDate,
                    TransactionNumber = transaction.TransactionNumber,
                    TransactionValue = transaction.TransactionValue
                },
                
                GetPrizeViewModel = new PrizeViewModel()
                {
                    PrizeName = prize.PrizeName
                }
            };

            if (formViewModel.GetTransactionViewModel.PrizeID == null)
            {
                formViewModel.GetPrizeViewModel.PrizeID = 1;
            }
            else
            {
                formViewModel.GetPrizeViewModel.PrizeID = Convert.ToInt32(formViewModel.GetTransactionViewModel.PrizeID);
            }

            return View(formViewModel);
        }

        [HttpPost]
        public ActionResult RedeemPrize(FormViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            foreach (var item in errors)
            {
                Debug.WriteLine(item.Key + " : " + item.Errors);
            }
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction
                {
                    CouponNumber = model.GetTransactionViewModel.CouponNumber,
                    CustomerID = model.GetTransactionViewModel.CustomerID,
                    PrizeID = model.GetTransactionViewModel.PrizeID,
                    PrizeRecieved = true,
                    TransactionDate = model.GetTransactionViewModel.TransactionDate,
                    TransactionID = model.GetTransactionViewModel.TransactionID,
                    TransactionNumber = model.GetTransactionViewModel.TransactionNumber,
                    TransactionValue = model.GetTransactionViewModel.TransactionValue
                };

                transactionRepository.ConfirmPrize(transaction.TransactionID);

                TempData["message"] = string.Format("{0} is saved", model.GetCustomerViewModel.PhoneNumber);
                return RedirectToAction("TransactionList");
            }
            else
            {
                return View("RedeemPrize", model);
            }
        }

        [HttpGet]
        public ActionResult ClaimedCouponsList(string searching = "")
        {
            return View(couponClaimPrizeRepository.CouponClaimPrizes.Where(x => x.CouponNumber.Contains(searching) || searching == null).ToList());
        }
    }
}