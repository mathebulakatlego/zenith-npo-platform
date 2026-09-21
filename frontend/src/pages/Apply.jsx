
import { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import PageContainer from '../components/layout/PageContainer'
import Section from '../components/layout/Section'

const MINIMUM_MOTIVATION_LENGTH = 150

function deriveDateOfBirth(idNumber) {
  if (!/^\d{6}/.test(idNumber)) {
    return ''
  }

  const year = Number(idNumber.slice(0, 2))
  const month = Number(idNumber.slice(2, 4))
  const day = Number(idNumber.slice(4, 6))
  const currentYear = new Date().getFullYear()
  const currentYearTwoDigits = currentYear % 100
  const currentCentury = Math.floor(currentYear / 100) * 100
  const fullYear =
    year <= currentYearTwoDigits
      ? currentCentury + year
      : currentCentury - 100 + year
  const date = new Date(Date.UTC(fullYear, month - 1, day))

  if (
    date.getUTCFullYear() !== fullYear ||
    date.getUTCMonth() !== month - 1 ||
    date.getUTCDate() !== day
  ) {
    return ''
  }

  return `${fullYear.toString().padStart(4, '0')}-${month
    .toString()
    .padStart(2, '0')}-${day.toString().padStart(2, '0')}`
}

function Apply() {
  const [programmes, setProgrammes] = useState([])
  const [programmesLoading, setProgrammesLoading] = useState(true)
  const [programmesError, setProgrammesError] = useState('')

  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    idNumber: '',
    dateOfBirth: '',
    email: '',
    phoneNumber: '',
    address: '',
    programmeId: '',
    motivation: '',
    idDocument: null,
    cv: null,
    qualification: null,
    consent: false,
  })

  const [isSubmitting, setIsSubmitting] = useState(false)
  const [submitError, setSubmitError] = useState('')
  const [submitSuccess, setSubmitSuccess] = useState(false)
  const motivationCharacterCount = formData.motivation.trim().length
  const motivationMinimumReached =
    motivationCharacterCount >= MINIMUM_MOTIVATION_LENGTH
  const derivedDateOfBirth = deriveDateOfBirth(formData.idNumber)
  const invalidIdDate =
    formData.idNumber.length >= 6 && !derivedDateOfBirth

  useEffect(() => {
    async function loadProgrammes() {
      try {
        const response = await fetch('/api/programmes')

        if (!response.ok) {
          throw new Error('Unable to load programmes.')
        }

        const data = await response.json()
        setProgrammes(data)
      } catch {
        setProgrammesError(
          'We could not load the available programmes. Please try again later.'
        )
      } finally {
        setProgrammesLoading(false)
      }
    }

    loadProgrammes()
  }, [])

  function handleChange(event) {
    const { name, value, type, checked, files } = event.target

    if (name === 'idNumber') {
      setFormData((current) => ({
        ...current,
        idNumber: value,
        dateOfBirth: deriveDateOfBirth(value),
      }))
      return
    }

    setFormData((current) => ({
      ...current,
      [name]:
        type === 'checkbox'
          ? checked
          : type === 'file'
            ? files[0] ?? null
            : value,
    }))
  }

  async function handleSubmit(event) {
    event.preventDefault()

    if (!motivationMinimumReached) {
      setSubmitError(
        `Please provide at least ${MINIMUM_MOTIVATION_LENGTH} meaningful characters in your motivation.`
      )
      return
    }

    setIsSubmitting(true)
    setSubmitError('')
    setSubmitSuccess(false)

    try {
      const applicantResponse = await fetch('/api/applicants', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          firstName: formData.firstName,
          lastName: formData.lastName,
          idNumber: formData.idNumber,
          dateOfBirth: formData.dateOfBirth,
          email: formData.email,
          phoneNumber: formData.phoneNumber,
          address: formData.address,
        }),
      })

      if (!applicantResponse.ok) {
        throw new Error('We could not save your personal details.')
      }

      const applicant = await applicantResponse.json()

      const applicationResponse = await fetch('/api/applications', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          applicantId: applicant.id,
          programmeId: Number(formData.programmeId),
          motivation: formData.motivation,
        }),
      })

      if (!applicationResponse.ok) {
        throw new Error('We could not create your application.')
      }

      const application = await applicationResponse.json()

      const documents = [
        {
          documentType: 'ID',
          file: formData.idDocument,
        },
        {
          documentType: 'CV',
          file: formData.cv,
        },
        {
          documentType: 'Qualification',
          file: formData.qualification,
        },
      ]

      for (const document of documents) {
        const uploadData = new FormData()

        uploadData.append('documentType', document.documentType)
        uploadData.append('file', document.file)

        const documentResponse = await fetch(
          `/api/applications/${application.id}/documents`,
          {
            method: 'POST',
            body: uploadData,
          }
        )

        if (!documentResponse.ok) {
          throw new Error(
            `We could not upload your ${document.documentType.toLowerCase()} document.`
          )
        }
      }

      setSubmitSuccess(true)

      setFormData({
        firstName: '',
        lastName: '',
        idNumber: '',
        dateOfBirth: '',
        email: '',
        phoneNumber: '',
        address: '',
        programmeId: '',
        motivation: '',
        idDocument: null,
        cv: null,
        qualification: null,
        consent: false,
      })
    } catch (error) {
      setSubmitError(
        error.message ||
          'Something went wrong while submitting your application. Please try again.'
      )
    } finally {
      setIsSubmitting(false)
    }
  }

  if (submitSuccess) {
    return (
      <main className="application-page">
        <Section>
          <PageContainer>
            <div className="application-success">
              <p className="section-eyebrow">Application Received</p>

              <h1>Thank you for applying.</h1>

              <p>
                Your application has been submitted successfully. Our team
                will review your information and supporting documents.
              </p>

              <p>
                Your application is currently <strong>Pending</strong>.
              </p>

              <NavLink to="/home" className="btn btn--primary">
                Return Home
              </NavLink>
            </div>
          </PageContainer>
        </Section>
      </main>
    )
  }

  return (
    <main className="application-page">
      <Section>
        <PageContainer>
          <div className="application-page-header">
            <p className="section-eyebrow">Application Form</p>

            <h1>Tell us about yourself</h1>

            <p>
              Please provide your details and supporting documents so that our
              team can review your application.
            </p>
          </div>

          {submitError && (
            <div className="application-form__submit-error" role="alert">
              {submitError}
            </div>
          )}

          <form className="application-form" onSubmit={handleSubmit}>
            <section className="application-form__section">
              <div className="application-form__heading">
                <span>01</span>

                <div>
                  <h3>Personal Details</h3>
                  <p>Your basic information and contact details.</p>
                </div>
              </div>

              <div className="application-form__grid">
                <div className="form-field">
                  <label htmlFor="firstName">First name</label>

                  <input
                    id="firstName"
                    name="firstName"
                    type="text"
                    autoComplete="given-name"
                    value={formData.firstName}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-field">
                  <label htmlFor="lastName">Surname</label>

                  <input
                    id="lastName"
                    name="lastName"
                    type="text"
                    autoComplete="family-name"
                    value={formData.lastName}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-field">
                  <label htmlFor="idNumber">
                    South African ID number
                  </label>

                  <input
                    id="idNumber"
                    name="idNumber"
                    type="text"
                    inputMode="numeric"
                    maxLength="13"
                    value={formData.idNumber}
                    onChange={handleChange}
                    required
                  />

                  {invalidIdDate && (
                    <small className="form-error">
                      The first six digits must represent a valid date of birth.
                    </small>
                  )}
                </div>

                <div className="form-field">
                  <label htmlFor="dateOfBirth">Date of birth</label>

                  <input
                    id="dateOfBirth"
                    name="dateOfBirth"
                    type="date"
                    value={formData.dateOfBirth}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-field">
                  <label htmlFor="email">Email address</label>

                  <input
                    id="email"
                    name="email"
                    type="email"
                    autoComplete="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-field">
                  <label htmlFor="phoneNumber">Phone number</label>

                  <input
                    id="phoneNumber"
                    name="phoneNumber"
                    type="tel"
                    autoComplete="tel"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-field application-form__field--full">
                  <label htmlFor="address">Address / area</label>

                  <input
                    id="address"
                    name="address"
                    type="text"
                    autoComplete="street-address"
                    value={formData.address}
                    onChange={handleChange}
                    required
                  />
                </div>
              </div>
            </section>

            <section className="application-form__section">
              <div className="application-form__heading">
                <span>02</span>

                <div>
                  <h3>Programme</h3>
                  <p>Select the programme you would like to apply for.</p>
                </div>
              </div>

              <div className="form-field">
                <label htmlFor="programmeId">Programme</label>

                <select
                  id="programmeId"
                  name="programmeId"
                  value={formData.programmeId}
                  onChange={handleChange}
                  required
                  disabled={
                    programmesLoading || Boolean(programmesError)
                  }
                >
                  <option value="" disabled>
                    {programmesLoading
                      ? 'Loading programmes...'
                      : 'Select a programme'}
                  </option>

                  {programmes.map((programme) => (
                    <option key={programme.id} value={programme.id}>
                      {programme.name}
                    </option>
                  ))}
                </select>

                {programmesError && (
                  <small className="form-error">
                    {programmesError}
                  </small>
                )}
              </div>
            </section>

            <section className="application-form__section">
              <div className="application-form__heading">
                <span>03</span>

                <div>
                  <h3>Motivation</h3>
                  <p>Tell us why you want to join the programme.</p>
                </div>
              </div>

              <div className="form-field">
                <label htmlFor="motivation">
                  Why should you be selected for this programme?
                </label>

                <textarea
                  id="motivation"
                  name="motivation"
                  rows="7"
                  maxLength="2000"
                  placeholder="Tell us about your goals, your interest in technology, and what you hope to gain from the programme."
                  value={formData.motivation}
                  onChange={handleChange}
                  required
                  aria-describedby="motivation-character-count"
                />

                <small
                  id="motivation-character-count"
                  className={`application-form__character-count ${motivationMinimumReached ? 'application-form__character-count--valid' : 'application-form__character-count--invalid'}`}
                  aria-live="polite"
                >
                  {motivationCharacterCount} / {MINIMUM_MOTIVATION_LENGTH}{' '}
                  characters ·{' '}
                  {motivationMinimumReached
                    ? 'Minimum reached'
                    : 'Minimum not reached'}
                </small>
              </div>
            </section>

            <section className="application-form__section">
              <div className="application-form__heading">
                <span>04</span>

                <div>
                  <h3>Supporting Documents</h3>
                  <p>
                    Upload the documents required to support your application.
                  </p>
                </div>
              </div>

              <div className="application-form__documents">
                <div className="form-field">
                  <label htmlFor="idDocument">
                    South African ID document
                  </label>

                  <input
                    id="idDocument"
                    name="idDocument"
                    type="file"
                    accept=".pdf,.png,.jpg,.jpeg"
                    onChange={handleChange}
                    required
                  />

                  <small>PDF, PNG or JPEG · Maximum 10 MB</small>
                </div>

                <div className="form-field">
                  <label htmlFor="cv">CV</label>

                  <input
                    id="cv"
                    name="cv"
                    type="file"
                    accept=".pdf,.png,.jpg,.jpeg"
                    onChange={handleChange}
                    required
                  />

                  <small>PDF, PNG or JPEG · Maximum 10 MB</small>
                </div>

                <div className="form-field">
                  <label htmlFor="qualification">
                    Matric certificate / relevant qualification
                  </label>

                  <input
                    id="qualification"
                    name="qualification"
                    type="file"
                    accept=".pdf,.png,.jpg,.jpeg"
                    onChange={handleChange}
                    required
                  />

                  <small>PDF, PNG or JPEG · Maximum 10 MB</small>
                </div>
              </div>
            </section>

            <div className="application-form__actions">
              <label className="application-form__consent">
                <input
                  type="checkbox"
                  name="consent"
                  checked={formData.consent}
                  onChange={handleChange}
                  required
                />

                <span>
                  I confirm that the information provided in this application
                  is accurate and complete, and I consent to Zenith Future
                  Skills Hub using the information and supporting documents
                  provided to assess my application.
                </span>
              </label>

              <button
                type="submit"
                className="btn btn--primary"
                disabled={isSubmitting}
              >
                {isSubmitting ? 'Submitting...' : 'Submit Application'}
              </button>
            </div>
          </form>
        </PageContainer>
      </Section>
    </main>
  )
}

export default Apply

