using Oyang.Ddd.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Ddd.Domain.UserAggregate
{
    public class UserAggregateRoot : Entity, IAggregateRoot
    {

        public UserAggregateRoot(
            string loginName,
            string nickName,
            string password
            )
        {
            this.Id = Guid.NewGuid();
            this.LoginName = loginName;
            this.NickName = nickName;
            this.SetPassword(password);
            this._isNew = true;
        }

        public UserAggregateRoot(
            Guid id,
            string loginName,
            string nickName,
            string passwordHash
            )
        {
            this.Id = id;
            this.LoginName = loginName;
            this.NickName = nickName;
            this.PasswordHash = passwordHash;
            this._isNew = false;
        }

        private readonly IUserRepository _repository;
        private bool _isNew;
        public string LoginName { get; private set; }
        public string NickName { get; private set; }
        public string PasswordHash { get; private set; }

        private List<Role> _roles = new List<Role>();
        public IReadOnlyCollection<Role> Roles => _roles;


        public void SetPassword(string password)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(password), "密码不能为空");
            this.PasswordHash = password;
        }

        public void AddRole(Role role)
        {
            if (!_roles.Contains(role) && !_roles.Exists(t => t.Id == role.Id))
            {
                _roles.Add(role);
            }
        }

        public void RemoveRole(Role role)
        {
            _roles.Remove(role);
        }

        public void Validate()
        {
            ValidationObject.Validate(this.Id == Guid.Empty, "id不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(this.LoginName), "登录名不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(this.PasswordHash), "密码不能为空");
            var isExistLoginName = _isNew ? _repository.IsExistLoginName(this.LoginName) : _repository.IsExistLoginName(this.LoginName, this.Id);
            ValidationObject.Validate(isExistLoginName, "登录名已存在");
        }

    }
}
