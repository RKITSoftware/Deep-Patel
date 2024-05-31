using Mail_API.Data;
using Mail_API.Dtos;
using Mail_API.Interface;
using Mail_API.Models;
using static Mail_API.Helper.ResponseHelper;

namespace Mail_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;

        public UserRepository(AppDbContext context, IEncryptionService encryptionService, ITokenService tokenService)
        {
            _context = context;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
        }

        public bool IsEmailExist(string email) => _context.Users.Any(predicate: u => u.Email.Equals(email));

        public bool IsUserExist(LoginUserDto loginUserDto)
        {
            string encryptedPassword = _encryptionService.Encrypt(loginUserDto.Password);

            return _context.Users.Any(predicate: u => u.Username == loginUserDto.Username && u.Password == encryptedPassword);
        }

        public bool IsUserNameExist(string userName) => _context.Users.Any(predicate: u => u.Username.Equals(userName));

        public Response LoginUser(LoginUserDto loginUserDto)
        {
            if (!IsUserExist(loginUserDto))
                return NotFoundResponse();

            User user = _context.Users.First(u => u.Username == loginUserDto.Username);

            string token = _tokenService.GetToken(user);
            return OkResponse("User successfully login.", token);
        }

        public Response RegisterUser(User user)
        {
            if (user == null) return BadRequestResponse();

            if (IsEmailExist(email: user.Email))
                return PreconditionFailedResponse("Email is already exists choose another email.");

            if (IsUserNameExist(userName: user.Username))
                return PreconditionFailedResponse("Username is already exists choose another email.");

            user.Password = _encryptionService.Encrypt(user.Password);
            _context.Users.Add(user);

            return SaveChanges() ? OkResponse("User successfully registered.") : InternalServerErrorResponse();
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
