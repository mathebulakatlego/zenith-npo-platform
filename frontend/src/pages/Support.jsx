import PageContainer from '../components/layout/PageContainer'
import Section from '../components/layout/Section'

function Support() {
  return (
    <main>
      <section className="page-hero">
        <PageContainer>
          <div className="page-hero__content">
            <p className="section-eyebrow">Get Involved</p>
            <h1>Support Zenith</h1>
            <p>
              Help create pathways into digital skills, opportunity and a
              better future.
            </p>
          </div>
        </PageContainer>
      </section>

      <Section>
        <PageContainer>
          <div className="support-actions">
            <article className="support-action">
              <p className="section-eyebrow">Volunteer</p>
              <h2>Share your time and skills.</h2>
              <p>
                Mentor, facilitate, or support learners on their journey.
              </p>
              <a className="btn btn--primary" href="#support-form">
                Volunteer With Us
              </a>
            </article>

            <article className="support-action">
              <p className="section-eyebrow">Partner</p>
              <h2>Create opportunities together.</h2>
              <p>
                Collaborate with Zenith through resources, opportunities, and
                shared impact.
              </p>
              <a className="btn btn--primary" href="#support-form">
                Partner With Us
              </a>
            </article>

            <article className="support-action">
              <p className="section-eyebrow">Support Our Work</p>
              <h2>Help expand access.</h2>
              <p>
                Provide resources and support that help learners access
                meaningful skills and opportunities.
              </p>
              <a className="btn btn--primary" href="#support-form">
                Support Our Work
              </a>
            </article>
          </div>
        </PageContainer>
      </Section>

      <Section className="section--light">
        <PageContainer>
          <div className="support-form-layout" id="support-form">
            <div>
              <p className="section-eyebrow">Get Started</p>
              <h2>Let's get you involved.</h2>
              <p>
                Tell us how you would like to support Zenith and we'll connect
                with you.
              </p>
            </div>

            <form className="support-form">
              <div className="form-field">
                <label htmlFor="support-name">Name</label>
                <input id="support-name" name="name" type="text" required />
              </div>

              <div className="form-field">
                <label htmlFor="support-email">Email</label>
                <input
                  id="support-email"
                  name="email"
                  type="email"
                  required
                />
              </div>

              <div className="form-field">
                <label htmlFor="support-phone">Phone</label>
                <input id="support-phone" name="phone" type="tel" />
              </div>

              <div className="form-field">
                <label htmlFor="support-type">How would you like to help?</label>
                <select
                  id="support-type"
                  name="supportType"
                  defaultValue=""
                  required
                >
                  <option value="" disabled>
                    Select an option
                  </option>
                  <option value="volunteer">Volunteer</option>
                  <option value="partner">Partner With Us</option>
                  <option value="support">Support Our Work</option>
                </select>
              </div>

              <div className="form-field">
                <label htmlFor="support-message">Message</label>
                <textarea
                  id="support-message"
                  name="message"
                  rows="5"
                  required
                />
              </div>

              <button className="btn btn--primary" type="submit">
                Send Enquiry
              </button>
            </form>
          </div>
        </PageContainer>
      </Section>
    </main>
  )
}

export default Support
